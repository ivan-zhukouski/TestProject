using GUI;
using UnityEngine;
using Zenject;

namespace GameStateMachine.StartGameState
{
    public class StartState : IState
    {
        [Inject] private GuiHandler _guiHandler;
        public static bool CanPlay = false;

        public void Enter()
        {
            if (!PlayerPrefs.HasKey("CurrLevel"))
            {
                PlayerPrefs.SetInt("CurrLevel", 1);
            }
            ActiveStartPanel();
            
            CanPlay = false;
            PlayerPrefs.SetInt("CountLevel", PlayerPrefs.GetInt("CountLevel") + 1);
        }

        private void ActiveStartPanel()
        {
            if (_guiHandler != null)
            {
                _guiHandler.SetGuiState(GuiHandler.GuiState.Start);
            }
        }

        public void Exit()
        {
            
        }
    }
}