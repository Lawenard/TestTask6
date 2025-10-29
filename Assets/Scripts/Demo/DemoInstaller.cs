namespace Demo
{
    using AvatarProvider;
    using Leaderboard;
    using SimplePopupManager;
    using Zenject;

    public class DemoInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAvatarProviderService>().To<AvatarProviderService>().AsSingle();
            Container.Bind<IPopupManagerService>().To<PopupManagerService>().AsSingle();
            Container.Bind<ILeaderboardService>().To<LeaderboardService>().AsSingle();
        }
    }
}
