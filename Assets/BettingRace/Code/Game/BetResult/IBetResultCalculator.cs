using BettingRace.Code.Services.PersistentProgress;

namespace BettingRace.Code.Game.BetResult
{
    public interface IBetResultCalculator : ISavedProgress
    {
        void SetBet(int bet);
        void CalculateBetResult(bool isWin);
    }
}