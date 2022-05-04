using BettingRace.Code.Data;
using BettingRace.Code.Services.Factories;
using BettingRace.Code.Services.PersistentProgress;

namespace BettingRace.Code.Infrastructure.StateMachine.States
{
    public class LoadGameState : IPayloadedState<bool>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly IPersistentProgressService _progressService;

        private bool _isBalanceReset;

        public LoadGameState(GameStateMachine stateMachine, SceneLoader sceneLoader, 
            IGameFactory gameFactory, IUIFactory uiFactory, IPersistentProgressService progressService)
        {
            _progressService = progressService;
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
        }

        public void Enter(bool isBalanceReset)
        {
            _isBalanceReset = isBalanceReset;
            _uiFactory.Cleanup();
            _sceneLoader.Load(SceneConstants.Game, OnGameSceneLoaded);
        }

        public void Exit()
        {
            if(_isBalanceReset) 
                _uiFactory.ResetBalanceLabel.Show();
        }

        private void InformProgress()
        {
            foreach (ISavedProgress progressWriters in _uiFactory.ProgressUsers)
                progressWriters.LoadProgress(_progressService.Progress);
        }

        private void OnGameSceneLoaded() =>
            _gameFactory.CreateGameComponents(CreateUI);

        private void CreateUI() =>
            _uiFactory.CreateUI(OnGameCreated);

        private void OnGameCreated()
        {
            InformProgress();
            _stateMachine.Enter<GameplayState>();
        }
    }
}