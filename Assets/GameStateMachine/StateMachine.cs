using System;
using System.Collections.Generic;
using System.Linq;

namespace GameStateMachine
{
    public class StateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _activeState;

        public StateMachine(List<IState> states)
        {
            _states = states.ToDictionary(state => state.GetType(), state => state);
        }

        public void Enter<TState>() where TState : IState
        {
            _activeState?.Exit();
            IState state = _states[typeof(TState)];
            _activeState = state;
            state.Enter();
        }
    }
}
