using System;
using UnityEngine;

namespace GUI.LosePanel
{
    [RequireComponent(typeof(LoseView))]
    public class LoseViewController : MonoBehaviour
    {
        public LoseView View => GetComponent<LoseView>();
        public event Action RestartEvent;
        
        private void OnEnable()
        {
            View.RestartButton.onClick.AddListener(OnRestartButtonClick);
        }

        private void OnDisable()
        {
            View.RestartButton.onClick.RemoveListener(OnRestartButtonClick);
        }

        private void OnRestartButtonClick()
        {
            RestartEvent?.Invoke();
        }
    }
}
