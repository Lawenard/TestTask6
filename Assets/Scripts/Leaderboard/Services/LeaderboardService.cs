namespace Leaderboard
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using UnityEngine;

    /// <summary>
    ///     Loads, stores, and refreshes leaderboard data
    /// </summary>
    public class LeaderboardService : ILeaderboardService
    {
        private const string FilePath = "Leaderboard";

        /// <summary>
        ///     The local state of the leaderboard, use <see cref="RefreshLeaderboardAsync"/> to update it.
        /// </summary>
        public IReadOnlyList<LeaderboardEntry> LeaderboardData => m_entries;

        private readonly List<LeaderboardEntry> m_entries = new();

        [Serializable]
        private struct LeaderboardEntryWrapper
        {
            public LeaderboardEntry[] leaderboard;
        }

        /// <summary>
        ///     Demo method that simply gets the JSON text from a local file and returns contents as a string.
        /// </summary>
        /// <exception cref="InvalidOperationException">Leaderboard.json not found</exception>
        private static async Task<string> FetchDataAsync()
        {
            ResourceRequest request = Resources.LoadAsync<TextAsset>(FilePath);
            // NOTE: we could also use request.completed if that flow is preferred
            while (!request.isDone) await Task.Yield();
            
            var textAsset = request.asset as TextAsset;
            return textAsset ? textAsset.text :
                throw new InvalidOperationException($"Resources/{FilePath} not found");
        }

        /// <summary>
        ///     Fetches leaderboard data and returns it.
        /// </summary>
        /// <exception cref="InvalidOperationException">Could not refresh leaderboard data</exception>
        public async Task<IReadOnlyList<LeaderboardEntry>> RefreshLeaderboardAsync()
        {
            var data = JsonConvert.DeserializeObject<LeaderboardEntryWrapper>(await FetchDataAsync());

            if (data.leaderboard != null)
            {
                m_entries.Clear();
                m_entries.AddRange(data.leaderboard);
            }
            else Debug.LogError("Could not refresh leaderboard data");

            return LeaderboardData;
        }
    }
}
