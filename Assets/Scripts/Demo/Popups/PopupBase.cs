namespace Demo.Popups
{
    using System.Threading.Tasks;
    using SimplePopupManager;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;
    
    /// <summary>
    ///     Base for popups that has built-in close functionality
    /// </summary>
    public abstract class PopupBase : MonoBehaviour, IPopupInitialization
    {
        protected abstract string Name { get; }
        
        [Inject] private IPopupManagerService m_popupManagerService;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button bgButton;

        private void Awake()
        {
            closeButton?.onClick.AddListener(ClosePopup);
            bgButton?.onClick.AddListener(ClosePopup);
        }

        protected void ClosePopup() => m_popupManagerService.ClosePopup(Name);

        public abstract Task Init(object param);
    }
}
