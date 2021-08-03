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

        public static event Action StartPlayGameEvent;
        
        [Inject] private CallBackState _callBackState;

        
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

        private void OnStartBtnClick()
        {
            StartPlayGameEvent?.Invoke();
        }
    }
}
