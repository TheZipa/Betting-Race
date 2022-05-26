using System;
using BettingRace.Code.Infrastructure;

namespace BettingRace.Code.Game.Horse
{
    public interface IHorseAccelerator
    {
        float GetAcceleration();
        void StartAccelerate(ICoroutineRunner coroutineRunner, Action onFullSpeed);
        void StartDecelerate(ICoroutineRunner coroutineRunner, Action onStop);
    }
}