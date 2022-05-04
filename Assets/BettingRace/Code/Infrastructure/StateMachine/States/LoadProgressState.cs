using BettingRace.Code.Data;
using BettingRace.Code.Services.PersistentProgress;
using BettingRace.Code.Services.SaveLoad;

namespace BettingRace.Code.Infrastructure.StateMachine.States
{
    public class LoadProgressState : IState
    {
        private const int FirstBalance = 5000;
        
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine stateMachine, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _progressService = progressService;
            _stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            LoadProgressOrInitNew();

            bool isZeroBalance = IsZeroBalance();
            if(isZeroBalance) ResetBalance();
            
            _stateMachine.Enter<LoadGameState, bool>(isZeroBalance);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrInitNew() =>
            _progressService.Progress = _saveLoadService.LoadProgress() ?? CreateNewProgress();

        private PlayerProgress CreateNewProgress() =>
            new PlayerProgress(FirstBalance);

        private void ResetBalance() =>
            _progressService.Progress.Balance = FirstBalance;

        private bool IsZeroBalance() =>
            _progressService.Progress.Balance == 0;
    }
}