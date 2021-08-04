using System;
using Entity.Player;
using GameStateMachine;
using GameStateMachine.StartGameState;
using GUI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Managers
{
    public class GameManager: ITickable, IInitializable, IDisposable
    {
        [Inject] private StateMachine _stateMachine;
        [Inject] private GuiHandler _gui;
        [Inject] private CallBackState _callBackState;
        [Inject] private PlayerInstaller _playerInstaller;
        
        private float _distanceToCamera = 10f;
        private Vector3 _touchPosInWarld;
        
        private PlayerBase _player => _playerInstaller.Player;
        
        private float xDeltaTouch = 0;
        
        private float _turnSpeed = 0.5f;

        private Camera _cam;

        public static event Action LoseGameEvent;

        public void Initialize()
        {
            _cam = Camera.main;
            _stateMachine.Enter<StartState>();
            _gui.SetGuiState(GuiHandler.GuiState.Start);
            
            _player.PlayerDie += PlayerDie;
            _gui.LoseViewController.RestartEvent += RestartGame;
            LoseGameEvent += _callBackState.EnterLoseState;
        }
        public void Dispose()
        {
            _gui.LoseViewController.RestartEvent -= RestartGame;
            LoseGameEvent -= _callBackState.EnterLoseState;
            _player.PlayerDie -= PlayerDie;
        }

        public void Tick()
        {
            PlayerOffset();
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private float _XrangeForPlayer = 2.4f;
        private void PlayerOffset()
        {
            if (StartState.CanPlay)
            {
                if(Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                   
                   if (Input.GetTouch(0).phase == TouchPhase.Began && touch.position.y < Screen.height / 3)
                   {
                       _touchPosInWarld = _cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y,_distanceToCamera ));
                       _player.Transform.position = new Vector3(_touchPosInWarld.x, _player.Transform.position.y,0 );
                   }
                   
                   if (touch.phase == TouchPhase.Moved)
                    {
                        xDeltaTouch = touch.deltaPosition.x;
                        Vector3 positionMultiplyer = new Vector3((xDeltaTouch * _turnSpeed) * Time.deltaTime, 0, 0);
                        float xValue;
                        Vector3 clampedPosition = _player.Transform.position;
                        clampedPosition += positionMultiplyer;
                        xValue = clampedPosition.x;
                        _player.Transform.position = new Vector3
                        (
                            xValue = Mathf.Clamp(xValue, -_XrangeForPlayer, _XrangeForPlayer),
                            clampedPosition.y,
                            clampedPosition.z
                        );
                    }
                }
                else
                {
                    xDeltaTouch = 0;
                }
            }
        }
        private void PlayerDie()
        {
            LoseGameEvent?.Invoke();
        }

        private void Finish()
        {
            _callBackState.EnterWinState();
        }
    }
}