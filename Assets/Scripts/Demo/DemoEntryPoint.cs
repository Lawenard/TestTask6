namespace Demo
{
    using AvatarProvider;
    using Leaderboard;
    using SimplePopupManager;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    /// <summary>
    ///     This demo's entry point for quickly demonstrating the leaderboard functionality
    /// </summary>
    public class DemoEntryPoint : MonoBehaviour
    {
        [SerializeField] private Canvas uiCanvas;
        
        [SerializeField] private Button leaderboardButton;
        [SerializeField] private Button clearCacheButton;
        [SerializeField] private string leaderboardPopupName;

        [Inject] private IAvatarProviderService m_avatarProviderService;
        [Inject] private IPopupManagerService m_popupManagerService;
        [Inject] private ILeaderboardService m_leaderboardService;
        
        private void Awake()
        {
            leaderboardButton.onClick.AddListener(OpenLeaderboard);
            clearCacheButton.onClick.AddListener(m_avatarProviderService.ClearCache);
        }

        private void OpenLeaderboard()
        {
            m_popupManagerService.OpenPopup(
                leaderboardPopupName,
                uiCanvas.transform,
                m_leaderboardService.RefreshLeaderboardAsync());
        }
    }
}
