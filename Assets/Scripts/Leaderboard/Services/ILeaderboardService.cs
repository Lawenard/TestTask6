namespace Leaderboard
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILeaderboardService
    {
        /// <summary>
        ///     The local state of the leaderboard, use <see cref="RefreshLeaderboardAsync"/> to update it.
        /// </summary>
        IReadOnlyList<LeaderboardEntry> LeaderboardData { get; }
        
        /// <summary>
        ///     Fetches leaderboard data and returns it.
        /// </summary>
        /// <exception cref="InvalidOperationException">Could not refresh leaderboard data</exception>
        Task<IReadOnlyList<LeaderboardEntry>> RefreshLeaderboardAsync();
    }
}
