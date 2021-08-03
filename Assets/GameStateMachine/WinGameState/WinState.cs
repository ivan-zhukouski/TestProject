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
            if (_guiHandler != null)
            {
                _guiHandler.SetGuiState(GuiHandler.GuiState.Win);
            }
            StartState.CanPlay = false;
            Debug.Log("WinState");
        }

        public void Exit()
        {
            
        }
    }
}