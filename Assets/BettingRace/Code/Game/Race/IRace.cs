using System;

namespace BettingRace.Code.Game.Race
{
    public interface IRace
    {
        void StartRace();
        void SetChosenCar(int carId);
        event Action<bool> OnRaceEnded;
        event Action<int, int> OnCarFinished;
    }
}