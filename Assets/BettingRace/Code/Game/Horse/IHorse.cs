using System;
using UnityEngine;

namespace BettingRace.Code.Game.Horse
{
    public interface IHorse
    {
        event Action<float, int> OnMoved;
        event Action<IHorse> OnFinish;
        int Id { get; }
        void Start();
        Transform GetTransform();
    }
}