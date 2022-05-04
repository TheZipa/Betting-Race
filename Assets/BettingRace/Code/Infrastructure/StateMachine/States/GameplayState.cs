using BettingRace.Code.Game.BetResult;
using BettingRace.Code.Game.Car;
using BettingRace.Code.Game.Race;
using BettingRace.Code.Services.Factories;
using BettingRace.Code.UI;
using BettingRace.Code.UI.Bet;
using BettingRace.Code.UI.FinishRace;
using BettingRace.Code.UI.SelectCarLayout;

namespace BettingRace.Code.Infrastructure.StateMachine.States
{
    public class GameplayState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;

        private IRace _race;
        private StartRacePanel _startRacePanel;
        private SelectCarLayoutGroup _selectCarLayoutGroup;
        private BetPanel _betPanel;
        private IBetModifier _betModifier;
        private FinishRacePanel _finishRacePanel;
        private FinishCarLayoutGroup _finishedCarList;
        private IBetResultCalculator _betResultCalculator;

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
            _race = _gameFactory.CreateRace(_uiFactory.CarProgressSliders);
            _startRacePanel = _uiFactory.StartRacePanel;
            _selectCarLayoutGroup = _uiFactory.SelectCarLayoutGroup;
            _betPanel = _uiFactory.BetPanel;
            _betModifier = _uiFactory.BetModifier;
            _finishRacePanel = _uiFactory.FinishRacePanel;
            _finishedCarList = _uiFactory.FinishedCarList;
            _betResultCalculator = _uiFactory.BetResultCalculator;
        }
        
        private void AddListeners()
        {
            _startRacePanel.OnStartRace += _race.StartRace;
            _startRacePanel.OnStartRace += _betModifier.ApproveBet;
            _selectCarLayoutGroup.OnCarSwitched += _race.SetChosenCar;
            _selectCarLayoutGroup.OnCarSwitched += _finishedCarList.SetChosenCar;
            _betPanel.OnAddBet += _betModifier.AddBet;
            _betPanel.OnClearBet += _betModifier.ClearBet;
            _betPanel.OnUndoBet += _betModifier.UndoBet;
            _betModifier.OnBetRefresh += _betPanel.RefreshTexts;
            _betModifier.OnBetApprove += _betResultCalculator.SetBet;
            _race.OnRaceEnded += _betResultCalculator.CalculateBetResult;
            _race.OnCarFinished += _finishedCarList.SetFinishedCarPosition;
            _finishRacePanel.OnNewRace += ReloadGame;
        }

        private void RemoveListeners()
        {
            _startRacePanel.OnStartRace -= _race.StartRace;
            _startRacePanel.OnStartRace -= _betModifier.ApproveBet;
            _selectCarLayoutGroup.OnCarSwitched -= _race.SetChosenCar;
            _selectCarLayoutGroup.OnCarSwitched -= _finishedCarList.SetChosenCar;
            _betPanel.OnAddBet -= _betModifier.AddBet;
            _betPanel.OnClearBet -= _betModifier.ClearBet;
            _betPanel.OnUndoBet -= _betModifier.UndoBet;
            _betModifier.OnBetRefresh -= _betPanel.RefreshTexts;
            _betModifier.OnBetApprove -= _betResultCalculator.SetBet;
            _race.OnRaceEnded -= _betResultCalculator.CalculateBetResult;
            _race.OnCarFinished -= _finishedCarList.SetFinishedCarPosition;
            _finishRacePanel.OnNewRace -= ReloadGame;
        }

        private void RefreshUI() =>
            _uiFactory.BetModifier.RefreshBetView();

        private void ReloadGame() =>
            _stateMachine.Enter<LoadProgressState>();
    }
}