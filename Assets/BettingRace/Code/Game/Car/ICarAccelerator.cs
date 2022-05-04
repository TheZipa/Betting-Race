using System;
using BettingRace.Code.Infrastructure;

namespace BettingRace.Code.Game.Car
{
    public interface ICarAccelerator
    {
        float GetAcceleration();
        void StartAccelerate(ICoroutineRunner coroutineRunner, Action onFullSpeed);
        void StartDecelerate(ICoroutineRunner coroutineRunner, Action onStop);
    }
}