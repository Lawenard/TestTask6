namespace Leaderboard
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    [Serializable]
    public class LeaderboardEntry
    {
        public string name;
        public long score;
        public string avatar;
        [JsonConverter(typeof(StringEnumConverter))]
        public LeaderboardEntryType type;

        public override string ToString() => $"{name} - {score} - {type.ToString()}; avatar: {avatar};";
    }

    public enum LeaderboardEntryType
    {
        Default = 0,
        Diamond,
        Gold,
        Silver,
        Bronze,
    }
}
