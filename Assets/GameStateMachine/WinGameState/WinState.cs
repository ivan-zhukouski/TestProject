using GameStateMachine.StartGameState;
using GUI;
using UnityEngine;
using Zenject;

namespace GameStateMachine.WinGameState
{
    public class WinState: IState
    {
        private GuiHandler _guiHandler;

        [Inject]
        private void Construct(GuiHandler guiHandler)
        {
            _guiHandler = guiHandler;
        }

        public void Enter()
        {
            ActiveWinPanel();

            StartState.CanPlay = false;
        }

        private void ActiveWinPanel()
        {
            if (_guiHandler != null)
            {
                _guiHandler.SetGuiState(GuiHandler.GuiState.Win);
            }
        }

        public void Exit()
        {
            
        }
    }
}