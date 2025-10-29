namespace AvatarProvider
{
    using System.Threading.Tasks;
    using UnityEngine;

    public interface IAvatarProviderService
    {
        /// <summary>
        ///     Downloads the avatar by url and caches it or loads it from cache if present.
        /// </summary>
        /// <param name="url">Url for the image. Must be a valid png image.</param>
        /// <param name="pixelsPerUnit">Sprite ppu scale.</param>
        Task<Sprite> GetSpriteAsync(string url, float pixelsPerUnit = 100f);
        
        /// <summary>
        ///     Delete all cached avatars from ImageCache dir
        /// </summary>
        void ClearCache();
    }
}
