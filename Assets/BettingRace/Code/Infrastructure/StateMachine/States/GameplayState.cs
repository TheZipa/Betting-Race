using BettingRace.Code.Game.BetResult;
using BettingRace.Code.Game.Environment.BackgroundParallax;
using BettingRace.Code.Game.Race;
using BettingRace.Code.Services.Factories.GameFactory;
using BettingRace.Code.Services.Factories.UIFactory;
using BettingRace.Code.UI.Bet;
using BettingRace.Code.UI.FinishRace;
using BettingRace.Code.UI.SelectHorseLayout;
using BettingRace.Code.UI.StartRace;

namespace BettingRace.Code.Infrastructure.StateMachine.States
{
    public class GameplayState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        private IRace _race;
        private StartRacePanel _startRacePanel;
        private StartRaceCountdown _startRaceCountdown;
        private SelectHorseLayoutGroup _selectHorseLayoutGroup;
        private ManageBetPanel _manageBetPanel;
        private IBetModifier _betModifier;
        private FinishRacePanel _finishRacePanel;
        private FinishHorseLayoutGroup _finishedHorseList;
        private IBetResultCalculator _betResultCalculator;
        private BackgroundParallax _backgroundParallax;

        public GameplayState(GameStateMachine stateMachine, IGameFactory gameFactory, IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
        }

        public void Enter() => PrepareGame();

        public void Exit() => RemoveListeners();

        private void PrepareGame()
        {
            AssignGameComponents();
            AddListeners();
            RefreshUI();
        }

        private void AssignGameComponents()
        {
            _race = _gameFactory.CreateRace(_uiFactory.HorseProgressSliders);
            _startRacePanel = _uiFactory.StartRacePanel;
            _startRaceCountdown = _uiFactory.StartRaceCountdown;
            _selectHorseLayoutGroup = _uiFactory.SelectHorseLayoutGroup;
            _manageBetPanel = _uiFactory.ManageBetPanel;
            _betModifier = _uiFactory.BetModifier;
            _finishRacePanel = _uiFactory.FinishRacePanel;
            _finishedHorseList = _uiFactory.FinishedHorseList;
            _betResultCalculator = _uiFactory.BetResultCalculator;
            _backgroundParallax = _gameFactory.BackgroundParallax;
        }
        
        private void AddListeners()
        {
            _startRacePanel.OnStartRace += _startRaceCountdown.StartCountdown;
            _startRacePanel.OnStartRace += _betModifier.ApproveBet;
            _startRaceCountdown.OnCountdownEnded += _race.StartRace;
            _startRaceCountdown.OnCountdownEnded += _backgroundParallax.Follow;
            _selectHorseLayoutGroup.OnHorseSwitched += _race.SetChosenHorse;
            _selectHorseLayoutGroup.OnHorseSwitched += _finishedHorseList.SetChosenHorse;
            _selectHorseLayoutGroup.OnHorseSwitched += _backgroundParallax.SetFollowing;
            _manageBetPanel.OnAddBet += _betModifier.AddBet;
            _manageBetPanel.OnClearBet += _betModifier.ClearBet;
            _manageBetPanel.OnUndoBet += _betModifier.UndoBet;
            _betModifier.OnBetRefresh += _manageBetPanel.RefreshTexts;
            _betModifier.OnBetApprove += _betResultCalculator.SetBet;
            _race.OnRaceEnded += _betResultCalculator.CalculateBetResult;
            _race.OnHorseFinished += _finishedHorseList.SetFinishedHorsePosition;
            _finishRacePanel.OnNewRace += ReloadGame;
        }

        private void RemoveListeners()
        {
            _startRacePanel.OnStartRace -= _startRaceCountdown.StartCountdown;
            _startRacePanel.OnStartRace -= _betModifier.ApproveBet;
            _startRaceCountdown.OnCountdownEnded -= _race.StartRace;
            _startRaceCountdown.OnCountdownEnded -= _backgroundParallax.Follow;
            _selectHorseLayoutGroup.OnHorseSwitched -= _race.SetChosenHorse;
            _selectHorseLayoutGroup.OnHorseSwitched -= _finishedHorseList.SetChosenHorse;
            _selectHorseLayoutGroup.OnHorseSwitched -= _backgroundParallax.SetFollowing;
            _manageBetPanel.OnAddBet -= _betModifier.AddBet;
            _manageBetPanel.OnClearBet -= _betModifier.ClearBet;
            _manageBetPanel.OnUndoBet -= _betModifier.UndoBet;
            _betModifier.OnBetRefresh -= _manageBetPanel.RefreshTexts;
            _betModifier.OnBetApprove -= _betResultCalculator.SetBet;
            _race.OnRaceEnded -= _betResultCalculator.CalculateBetResult;
            _race.OnHorseFinished -= _finishedHorseList.SetFinishedHorsePosition;
            _finishRacePanel.OnNewRace -= ReloadGame;
        }

        private void RefreshUI() =>
            _uiFactory.BetModifier.RefreshBetView();

        private void ReloadGame() =>
            _stateMachine.Enter<LoadProgressState>();
    }
}