using GUI;
using UnityEngine;
using Zenject;

namespace GameStateMachine.StartGameState
{
    public class StartState : IState
    {
        [Inject] private GuiHandler _guiHandler;
        public static bool CanPlay = false;
        public static bool IsStartPlayGame = false;
       
        public void Enter()
        {
            if (!PlayerPrefs.HasKey("CurrLevel"))
            {
                PlayerPrefs.SetInt("CurrLevel", 1);
            }
            IsStartPlayGame = true;
            CanPlay = false;
            PlayerPrefs.SetInt("CountLevel", PlayerPrefs.GetInt("CountLevel") + 1);
            Debug.Log("Start State");
        }

        public void Exit()
        {
            
        }
    }
}