using System;
using UnityEngine;

namespace GUI.WinPanel
{
    [RequireComponent(typeof(WinView))]
    public class WinViewController : MonoBehaviour
    {
        public WinView View => GetComponent<WinView>();

        public event Action NextLevelEvent;

        private void OnEnable()
        {
            View.NextLevelBtn.onClick.AddListener(OnNextLevelBtnClick);
        }

        private void OnDisable()
        {
            View.NextLevelBtn.onClick.RemoveListener(OnNextLevelBtnClick);
        }

        private void OnNextLevelBtnClick()
        {
            NextLevelEvent?.Invoke();
        }
    }
}
