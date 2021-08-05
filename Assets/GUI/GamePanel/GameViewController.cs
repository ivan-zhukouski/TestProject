using System;
using GameStateMachine;
using UnityEngine;
using Zenject;

namespace GUI.GamePanel
{
    [RequireComponent(typeof(GameView))]
    public class GameViewController : MonoBehaviour
    {
        public GameView View => GetComponent<GameView>();
        
        [Inject] private GuiHandler _guiHandler;
        [Inject] private CallBackState _callBackState;
        public event Action GoToMenuEvent;
        public void ActiveTimer(bool isTurnOn)
        {
            View.TimePanel.gameObject.SetActive(isTurnOn);
        }
        private void OnEnable()
        {
            GoToMenuEvent += _callBackState.EnterMenuEvent;
            View.Menu.onClick.AddListener(OnMenuBtnClick);
        }

        private void OnDisable()
        {
            GoToMenuEvent -= _callBackState.EnterMenuEvent;
            View.Menu.onClick.RemoveListener(OnMenuBtnClick);
        }

        private void OnMenuBtnClick()
        {
            GoToMenuEvent?.Invoke();
            _guiHandler.MenuViewController.gameObject.SetActive(true);
        }
    }
}
