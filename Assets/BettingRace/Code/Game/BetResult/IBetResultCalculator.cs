using BettingRace.Code.Services.PersistentProgress;

namespace BettingRace.Code.Game.BetResult
{
    public interface IBetResultCalculator : ISaveLoadProgress
    {
        void SetBet(int bet);
        void CalculateBetResult(bool isWin);
    }
}