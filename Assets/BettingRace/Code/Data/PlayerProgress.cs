using System;

namespace BettingRace.Code.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public int Balance;

        public PlayerProgress(int balance) =>
            Balance = balance;
    }
}