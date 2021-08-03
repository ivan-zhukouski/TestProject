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
            if (_guiHandler != null)
            {
                _guiHandler.SetGuiState(GuiHandler.GuiState.Lose);
            }
            StartState.CanPlay = false;
            Debug.Log("LoseState");
        }

        public void Exit()
        {
            
        }
    }
}