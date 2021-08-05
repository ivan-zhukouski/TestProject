using GameStateMachine.StartGameState;
using GUI;
using UnityEngine;
using Zenject;

namespace GameStateMachine.LoseGameState
{
    public class LoseState: IState
    {
        private GuiHandler _guiHandler;
        
        [Inject]
        private void Construct(GuiHandler guiHandler)
        {
            _guiHandler = guiHandler;
        }
        
        public void Enter()
        {
            ActiveLosePanel();
            StartState.CanPlay = false;
        }

        public void ActiveLosePanel()
        {
            if (_guiHandler != null)
            {
                _guiHandler.SetGuiState(GuiHandler.GuiState.Lose);
            }
        }

        public void Exit()
        {
            
        }
    }
}