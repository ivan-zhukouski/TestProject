using System;
using GUI.GamePanel;
using GUI.LosePanel;
using GUI.StartPanel;
using GUI.WinPanel;
using UnityEngine;

namespace GUI
{
    public class GuiHandler : MonoBehaviour
    {
        public enum GuiState
        {
            Start,
            Game, 
            Win,
            Lose
        }
        
        public StartViewController StartViewController;
        public GameViewController GameViewController;
        public LoseViewController LoseViewController;
        public WinViewController WinViewController;

        public void SetGuiState(GuiState state)
        {
            switch (state)
            {
                case GuiState.Start:
                    StartViewController.gameObject.SetActive(true);
                    GameViewController.gameObject.SetActive(false);
                    LoseViewController.gameObject.SetActive(false);
                    WinViewController.gameObject.SetActive(false);
                    break;
                case GuiState.Game:
                    StartViewController.gameObject.SetActive(false);
                    GameViewController.gameObject.SetActive(true);
                    LoseViewController.gameObject.SetActive(false);
                    WinViewController.gameObject.SetActive(false);
                    break;
                case GuiState.Win:
                    StartViewController.gameObject.SetActive(false);
                    GameViewController.gameObject.SetActive(false);
                    LoseViewController.gameObject.SetActive(false);
                    WinViewController.gameObject.SetActive(true);
                    break;
                case GuiState.Lose:
                    StartViewController.gameObject.SetActive(false);
                    GameViewController.gameObject.SetActive(false);
                    LoseViewController.gameObject.SetActive(true);
                    WinViewController.gameObject.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}
