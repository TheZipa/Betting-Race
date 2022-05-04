using System;
using UnityEngine;

namespace BettingRace.Code.Game.Car
{
    public interface ICar
    {
        event Action<float, int> OnMoved;
        event Action<ICar> OnFinish;
        int Id { get; }
        void Start();
        Transform GetTransform();
    }
}