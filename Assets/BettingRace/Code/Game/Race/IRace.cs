using System;

namespace BettingRace.Code.Game.Race
{
    public interface IRace
    {
        void StartRace();
        void SetChosenHorse(int horseId);
        event Action<bool> OnRaceEnded;
        event Action<int, int> OnHorseFinished;
    }
}