using System;
using System.Collections.Generic;
using BettingRace.Code.Game.BetResult;
using BettingRace.Code.Infrastructure.DI;
using BettingRace.Code.Services.PersistentProgress;
using BettingRace.Code.UI;
using BettingRace.Code.UI.Bet;
using BettingRace.Code.UI.FinishRace;
using BettingRace.Code.UI.SelectCarLayout;

namespace BettingRace.Code.Services.Factories
{
    public interface IUIFactory : IService
    {
        SelectCarLayoutGroup SelectCarLayoutGroup { get; }
        StartRacePanel StartRacePanel { get; }
        IBetModifier BetModifier { get; }
        BetPanel BetPanel { get; }
        ResetBalanceLabel ResetBalanceLabel { get; }
        FinishRacePanel FinishRacePanel { get; }
        FinishCarLayoutGroup FinishedCarList { get; }
        IBetResultCalculator BetResultCalculator { get; }
        RaceProgressSliderGroup CarProgressSliders { get; }
        List<ISavedProgress> ProgressUsers { get; }

        void CreateUI(Action onCreated);
        void Cleanup();
    }
}