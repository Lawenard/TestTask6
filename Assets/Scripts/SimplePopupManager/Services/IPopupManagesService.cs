//© 2023 Sophun Games LTD. All rights reserved.
//This code and associated documentation are proprietary to Sophun Games LTD.
//Any use, reproduction, distribution, or release of this code or documentation without the express permission
//of Sophun Games LTD is strictly prohibited and could be subject to legal action.

namespace SimplePopupManager
{
    using UnityEngine;

    public interface IPopupManagerService
    {
        /// <summary>
        ///     Opens a popup by its name and initializes it with the given parameters.
        ///     If the popup is already loaded, it will log an error and return.
        /// </summary>
        /// <param name="name">The name of the popup to open.</param>
        /// <param name="parent">The parent that has <see cref="UnityEngine.Canvas"/> on top of its hierarchy</param>
        /// <param name="param">The parameters to initialize the popup with.</param>
        void OpenPopup(string name, Transform parent, object param);

        /// <summary>
        ///     Closes a popup by its name.
        ///     If the popup is loaded, it will release its instance and remove it from the dictionary.
        /// </summary>
        /// <param name="name">The name of the popup to close.</param>
        void ClosePopup(string name);
    }
}