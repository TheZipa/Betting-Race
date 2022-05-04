using System;
using BettingRace.Code.Services.PersistentProgress;

namespace BettingRace.Code.UI.Bet
{
    public interface IBetModifier : ISavedProgress
    {
        event Action<string, string> OnBetRefresh;
        event Action<int> OnBetApprove;

        void AddBet(int bet);
        void UndoBet();
        void ClearBet();
        void ApproveBet();
        void RefreshBetView();
    }
}