namespace Demo.Popups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AvatarProvider;
    using Leaderboard;
    using UiElements;
    using UnityEngine;
    using Zenject;

    public class LeaderboardPopup : PopupBase
    {
        [Inject] private IAvatarProviderService m_avatarProviderService;
        
        protected override string Name => nameof(LeaderboardPopup);
        
        [Serializable]
        private struct LeaderboardEntryViewSetting
        {
            [field:SerializeField] public LeaderboardEntryType Type { get; private set; }
            [field:SerializeField] public Color Color { get; private set; }
            [field:SerializeField] public float Height { get; private set; }
        }
        
        [SerializeField] private LeaderboardEntryViewSetting[] leaderboardEntryViewSettings;
        [SerializeField] private LeaderboardEntryView leaderboardEntryViewPrefab;
        
        public override async Task Init(object param)
        {
            if (param is Task<IReadOnlyList<LeaderboardEntry>> leaderboardData)
            {
                foreach (LeaderboardEntry item in await leaderboardData)
                {
                    LeaderboardEntryView leaderboardEntryView = Instantiate(
                        leaderboardEntryViewPrefab, leaderboardEntryViewPrefab.transform.parent);

                    LeaderboardEntryViewSetting setting = leaderboardEntryViewSettings.FirstOrDefault(
                        s => s.Type == item.type);
                    leaderboardEntryView.Init(item, setting.Color, setting.Height);
                    leaderboardEntryView.gameObject.SetActive(true);
                    SetAvatar(leaderboardEntryView, item.avatar);
                }
            }
            else throw new Exception("param is not Task<IReadOnlyList<LeaderboardEntry>>");
        }

        private async void SetAvatar(LeaderboardEntryView leaderboardEntryView, string avatarUrl)
        {
            Sprite sprite = await m_avatarProviderService.GetSpriteAsync(avatarUrl);
            leaderboardEntryView.SetAvatar(sprite);
        }
    }
}
