namespace AvatarProvider
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.Networking;

    public class AvatarProviderService : IAvatarProviderService
    {
        private readonly string m_cacheDir;
        private readonly Sprite m_fallbackSprite;

        public AvatarProviderService()
        {
            m_cacheDir = Path.Combine(Application.persistentDataPath, "ImageCache");
            CreateCacheDir();
        }

        private string GetCachePath(string url)
        {
            string hash = ComputeHash(url);
            string filename = hash + ".png";
            return Path.Combine(m_cacheDir, filename);
        }

        private void CreateCacheDir()
        {
            if (!Directory.Exists(m_cacheDir))
            {
                Directory.CreateDirectory(m_cacheDir);
            }
        }

        private static string ComputeHash(string input)
        {
            using var sha1 = SHA1.Create();
            byte[] bytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        private async Task<Texture2D> GetTextureAsync(string url)
        {
            string path = GetCachePath(url);
            if (File.Exists(path))
            {
                try
                {
                    byte[] bytes = await Task.Run(() => File.ReadAllBytes(path));
                    var tex = new Texture2D(2, 2);
                    if (tex.LoadImage(bytes))
                    {
                        return tex;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Failed to load cached image: {e}");
                }
            }
            
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url))
            {
                UnityWebRequestAsyncOperation asyncOp = uwr.SendWebRequest();
                while (!asyncOp.isDone)
                {
                    await Task.Yield();
                }

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Image download error for {url}: {uwr.error}");
                    return null;
                }

                Texture2D tex = DownloadHandlerTexture.GetContent(uwr);
                if (tex == null)
                {
                    Debug.LogError($"Downloaded texture is null for {url}");
                    return null;
                }
                
                CreateCacheDir();
                try
                {
                    byte[] png = tex.EncodeToPNG();
                    await Task.Run(() => File.WriteAllBytes(path, png));
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Failed to save image cache for {url}: {e}");
                }

                return tex;
            }
        }

        /// <summary>
        ///     Downloads the avatar by url and caches it or loads it from cache if present.
        /// </summary>
        /// <param name="url">Url for the image. Must be a valid png image.</param>
        /// <param name="pixelsPerUnit">Sprite ppu scale.</param>
        public async Task<Sprite> GetSpriteAsync(string url, float pixelsPerUnit = 100f)
        {
            Texture2D tex = await GetTextureAsync(url);
            if (tex == null) return null; // TODO: Add fallback img :)
            return Sprite.Create(
                tex,
                new Rect(0, 0, tex.width, tex.height),
                new Vector2(0.5f, 0.5f), pixelsPerUnit);
        }
        
        /// <summary>
        ///     Delete all cached avatars from ImageCache dir
        /// </summary>
        public void ClearCache()
        {
            if (Directory.Exists(m_cacheDir))
            {
                Directory.Delete(m_cacheDir, true);
            }
        }
    }
}
