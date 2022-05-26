using System;
using System.Collections.Generic;
using BettingRace.Code.Game.BetResult;
using BettingRace.Code.Infrastructure.DI;
using BettingRace.Code.Services.PersistentProgress;
using BettingRace.Code.UI;
using BettingRace.Code.UI.Bet;
using BettingRace.Code.UI.FinishRace;
using BettingRace.Code.UI.SelectHorseLayout;
using BettingRace.Code.UI.Settings;
using BettingRace.Code.UI.StartRace;

namespace BettingRace.Code.Services.Factories.UIFactory
{
    public interface IUIFactory : IService
    {
        SelectHorseLayoutGroup SelectHorseLayoutGroup { get; }
        StartRacePanel StartRacePanel { get; }
        StartRaceCountdown StartRaceCountdown { get; }
        IBetModifier BetModifier { get; }
        ManageBetPanel ManageBetPanel { get; }
        ResetBalanceLabel ResetBalanceLabel { get; }
        FinishRacePanel FinishRacePanel { get; }
        FinishHorseLayoutGroup FinishedHorseList { get; }
        IBetResultCalculator BetResultCalculator { get; }
        RaceProgressSliderGroup HorseProgressSliders { get; }
        GameSettings GameSettings { get; }
        List<ISaveLoadProgress> ProgressUsers { get; }

        void CreateUI(Action onCreated);
        void Cleanup();
    }
}