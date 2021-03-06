using System;
using System.Collections.Generic;
using BettingRace.Code.Infrastructure.DI;
using BettingRace.Code.Infrastructure.StateMachine.States;
using BettingRace.Code.Services.Factories.GameFactory;
using BettingRace.Code.Services.Factories.UIFactory;
using BettingRace.Code.Services.PersistentProgress;
using BettingRace.Code.Services.SaveLoad;
using BettingRace.Code.Services.Sound;

namespace BettingRace.Code.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, SoundService soundService)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, soundService),
                [typeof(LoadProgressState)] = new LoadProgressState(this,
                        AllServices.Container.Single<IPersistentProgressService>(), 
                        AllServices.Container.Single<ISaveLoadService>()),
                [typeof(LoadGameState)] = new LoadGameState(this, sceneLoader, 
                    AllServices.Container.Single<IGameFactory>(), 
                    AllServices.Container.Single<IUIFactory>(), 
                    AllServices.Container.Single<IPersistentProgressService>()),
                [typeof(GameplayState)] = new GameplayState(this, AllServices.Container.Single<IGameFactory>(), 
                    AllServices.Container.Single<IUIFactory>())
            };
        }
        
        public void Enter<TState>() where TState : class, IState =>
            ChangeState<TState>().Enter();

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload> =>
            ChangeState<TState>().Enter(payload);

        private TState GetState<TState>() where TState : class, IExitableState 
            => _states[typeof(TState)] as TState;

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        ~GameStateMachine() => _activeState.Exit();
    }
}