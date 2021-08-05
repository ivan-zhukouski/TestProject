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

        private Vector3 _touchPosInWorld;
        private PlayerBase _player => _playerInstaller.Player;

        private float _distanceToCamera = 10f;
        private float _xDeltaTouch = 0;
        private float _turnSpeed = 0.3f;
        private float _currentScore = 0;
        private float _XrangeForPlayer = 2.4f;
        private float _timer = 2f;
        
        private bool _canActiveLoseState = false;

        private Camera _cam;
        
        public static event Action LoseGameEvent;

        public void Initialize()
        {
            _gui.SetGuiState(GuiHandler.GuiState.Start);
            
            if (!PlayerPrefs.HasKey("BestScore"))
            {
                PlayerPrefs.SetFloat("BestScore", 0);
            }

            _cam = Camera.main;
            _stateMachine.Enter<StartState>();

            _player.PlayerDie += PlayerDie;
            _gui.LoseViewController.RestartEvent += RestartGame;
            LoseGameEvent += _callBackState.EnterLoseState;
            _gui.GameViewController.GoToMenuEvent += SetScoreToMenuEvent;
            _gui.MenuViewController.TryAgainEvent += RestartGame;
        }
        public void Dispose()
        {
            _gui.LoseViewController.RestartEvent -= RestartGame;
            LoseGameEvent -= _callBackState.EnterLoseState;
            _player.PlayerDie -= PlayerDie;
            _gui.GameViewController.GoToMenuEvent -= SetScoreToMenuEvent;
            _gui.MenuViewController.TryAgainEvent -= RestartGame;
        }

        public void Tick()
        {
            PlayerOffset();
            ScoreCounter();
            ActiveLosePanelAfterTime();
            StartPlayGameWithTouch();
        }

        private void StartPlayGameWithTouch()
        {
            if (!StartState.CanPlay)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (Input.GetTouch(0).phase == TouchPhase.Began || touch.phase == TouchPhase.Moved && touch.position.y < Screen.height / 3.5f)
                    {
                        _gui.StartViewController.OnStartBtnClick();
                    }
                }
            }
        }

        private void SetScoreToMenuEvent()
        {
            SaveScore();
            _gui.MenuViewController.View.ScoreMenu.ScoreText.text = _currentScore.ToString("F1").Replace("." , "");
            _gui.MenuViewController.View.ScoreMenu.BestScoreText.text = PlayerPrefs.GetFloat("BestScore").ToString("F1").Replace(".", "");
        }

        private void SetScoreToFinish()
        {
            SaveScore();
            _gui.LoseViewController.View.CurrentScore.text = _currentScore.ToString("F1").Replace(".", "");
            _gui.LoseViewController.View.BestScore.text = PlayerPrefs.GetFloat("BestScore").ToString("F1").Replace(".", "");
        }


        private void ActiveLosePanelAfterTime()
        {
            if (_canActiveLoseState)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    _canActiveLoseState = false;
                    _timer = 2f;
                    ActiveLoseState();
                }
            }
        }


        private void ScoreCounter()
        {
            if (StartState.CanPlay)
            {
                _currentScore += Time.deltaTime;
                _gui.GameViewController.View.ScoreCount.text = _currentScore.ToString("F1").Replace(".", "");
            }
        }

       
        private void PlayerDie()
        {
            StartState.CanPlay = false;
            _canActiveLoseState = true;
        }

        private void ActiveLoseState()
        {
            LoseGameEvent?.Invoke();
            SetScoreToFinish();
        }
        
        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void PlayerOffset()
        {
            if (StartState.CanPlay)
            {
                if(Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                   
                   if (Input.GetTouch(0).phase == TouchPhase.Began && touch.position.y < Screen.height / 3.5f)
                   {
                       _touchPosInWorld = _cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y,_distanceToCamera ));
                       _player.Transform.position = new Vector3(_touchPosInWorld.x, _player.Transform.position.y,0 );
                   }
                   
                   if (touch.phase == TouchPhase.Moved && touch.position.y < Screen.height / 3.5f)
                    {
                        _xDeltaTouch = touch.deltaPosition.x;
                        Vector3 positionMultiplyer = new Vector3((_xDeltaTouch * _turnSpeed) * Time.deltaTime, 0, 0);
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
                    _xDeltaTouch = 0;
                }
            }
        }

        private void SaveScore()
        {
            if (PlayerPrefs.GetFloat("BestScore") < _currentScore)
            {
                PlayerPrefs.SetFloat("BestScore", _currentScore);
            }
        }
    }
}