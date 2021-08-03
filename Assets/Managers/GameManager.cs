using System;
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
        
        private int _currentLevel => PlayerPrefs.GetInt("Level");

        private const int MAX_COUNT_LOADING_LEVEL =14; 

        private const int MAX_RAND_INDEX_LEVEL = 14;
    
        private const int MIN_RAND_INDEX_LEVEL = 4;

        public static event Action LoseGameEvent;

        public void Initialize()
        {
            _stateMachine.Enter<StartState>();
            _gui.SetGuiState(GuiHandler.GuiState.Start);
            if (!PlayerPrefs.HasKey("LoopIndex"))
            {
                PlayerPrefs.SetInt("LoopIndex", MIN_RAND_INDEX_LEVEL);
            }
            _gui.LoseViewController.RestartEvent += RestartGame;
            _gui.WinViewController.NextLevelEvent += NextLevel;
           
            LoseGameEvent += _callBackState.EnterLoseState;
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void NextLevel()
        {
            PlayNextLevel();
        }

        private void PlayNextLevel()
        {
            SaveGame();
            PlayerPrefs.SetInt("CurrLevel", PlayerPrefs.GetInt("CurrLevel") + 1);
            if (_currentLevel <= MAX_COUNT_LOADING_LEVEL)
            {
                LoadNextLevel();
            }
            else if (_currentLevel > MAX_COUNT_LOADING_LEVEL)
            {
                LoadRandomLvlInRange(MIN_RAND_INDEX_LEVEL, MAX_RAND_INDEX_LEVEL);
            }
        }
        private void LoadRandomLvlInRange(int min, int max)
        {
           
            if (PlayerPrefs.GetInt("LevelLoop") == 1)
                PlayerPrefs.SetInt("LevelLoop", PlayerPrefs.GetInt("LevelLoop") + 1);
            if (PlayerPrefs.GetInt("LoopIndex") > max)
            {
                PlayerPrefs.SetInt("LoopIndex", min);
                PlayerPrefs.SetInt("LevelLoop", PlayerPrefs.GetInt("LevelLoop") + 1);
            
            }
            PlayerPrefs.SetInt("LoopIndex", PlayerPrefs.GetInt("LoopIndex") + 1);
            SceneManager.LoadScene(PlayerPrefs.GetInt("LoopIndex"));
        }
        
        private void SaveGame()
        {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            PlayerPrefs.Save();
        }

        private void LoadNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void Tick()
        {
        }

        private void PlayerDie()
        {
            LoseGameEvent?.Invoke();
        }

        private void Finish()
        {
            _callBackState.EnterWinState();
        }
        
        

        public void Dispose()
        {
            _gui.LoseViewController.RestartEvent -= RestartGame;
            _gui.WinViewController.NextLevelEvent -= NextLevel;
            LoseGameEvent -= _callBackState.EnterLoseState;
        }
    }
}