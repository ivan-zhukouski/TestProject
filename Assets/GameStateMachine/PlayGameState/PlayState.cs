using GameStateMachine.StartGameState;
using GUI;
using UnityEngine;
using Zenject;

namespace GameStateMachine.PlayGameState
{
    public class PlayState : IState
    {   
        private GuiHandler _guiHandler;
        
        [Inject]
        private void Construct(GuiHandler guiHandler)
        {
            _guiHandler = guiHandler;
        }
        public void Enter()
        {
            StartState.CanPlay = true;
            ActivePlayPanel();
        }

        private void ActivePlayPanel()
        {
            if (_guiHandler != null)
            {
                _guiHandler.SetGuiState(GuiHandler.GuiState.Game);
            }
        }

        public void Exit()
        {
            
        }
    }
}