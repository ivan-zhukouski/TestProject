using Entity.Player;
using GameStateMachine;
using GameStateMachine.LoseGameState;
using GameStateMachine.PlayGameState;
using GameStateMachine.StartGameState;
using GameStateMachine.WinGameState;
using GUI;
using Managers;
using Zenject;

namespace SceneContextDI
{
    public class GameSceneContext : ScriptableObjectInstaller<GameSceneContext>
    {
        public override void InstallBindings()
        {
            BindStateMachine();
            BindGameManager();
            BindGui();
            BindPlayerInstaller();
        }
        private void BindStateMachine()
        {
            Container.Bind<IState>().To<StartState>().AsCached();
            Container.Bind<IState>().To<PlayState>().AsCached();
            Container.Bind<IState>().To<LoseState>().AsCached();
            Container.Bind<IState>().To<WinState>().AsCached();
            Container.Bind<CallBackState>().AsCached();
            Container.BindInterfacesAndSelfTo<StateMachine>().AsSingle().NonLazy();
        }

        private void BindGameManager()
        {
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
        }
        private void BindGui()
        {
            Container.Bind<GuiHandler>().FromComponentInHierarchy(true).AsSingle();
        }

        private void BindPlayerInstaller()
        {
            Container.Bind<PlayerInstaller>().FromComponentInHierarchy(true).AsSingle();
        }
    }
}