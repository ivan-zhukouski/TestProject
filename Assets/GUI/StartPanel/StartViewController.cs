using System;
using GameStateMachine;
using UnityEngine;
using Zenject;

namespace GUI.StartPanel
{
    [RequireComponent(typeof(StartView))]
    public class StartViewController : MonoBehaviour
    {
        public StartView View => GetComponent<StartView>();

        public event Action StartPlayGameEvent;
        
        [Inject] private CallBackState _callBackState;

        public void OnStartBtnClick()
        {
            StartPlayGameEvent?.Invoke();
        }

        private void OnEnable()
        {
            StartPlayGameEvent += _callBackState.EnterPlayState;
            View.PlayBnt.onClick.AddListener(OnStartBtnClick);
        }

        private void OnDisable()
        {
            StartPlayGameEvent -= _callBackState.EnterPlayState;
            View.PlayBnt.onClick.RemoveListener(OnStartBtnClick);
        }
    }
}
