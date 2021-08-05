using System;
using UnityEngine;

namespace GUI.MenuPanel
{
    public class MenuViewController : MonoBehaviour
    {
        public MenuView View => GetComponent<MenuView>();

        public event Action TryAgainEvent;
        private void OnEnable()
        {
            View.MainMenu.TryAgainBtn.onClick.AddListener(OnTryAgainBtnClick);
            View.MainMenu.ScoreBtn.onClick.AddListener(GoToScoreMenu);
            View.ScoreMenu.BackBtn.onClick.AddListener(GoToMainMenu);
        }

        private void OnDisable()
        {
            View.MainMenu.TryAgainBtn.onClick.RemoveListener(OnTryAgainBtnClick);
            View.MainMenu.ScoreBtn.onClick.RemoveListener(GoToScoreMenu);
            View.ScoreMenu.BackBtn.onClick.RemoveListener(GoToMainMenu);
        }

        private void GoToScoreMenu()
        {
            View.ScoreMenu.gameObject.SetActive(true);
            View.MainMenu.gameObject.SetActive(false);
        }
        private void GoToMainMenu()
        {
            View.ScoreMenu.gameObject.SetActive(false);
            View.MainMenu.gameObject.SetActive(true);
        }
        
        private void OnTryAgainBtnClick()
        {
            TryAgainEvent?.Invoke();
        }
    }
}
