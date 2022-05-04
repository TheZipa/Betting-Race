using System;
using System.Collections.Generic;
using BettingRace.Code.Game.Horse;
using BettingRace.Code.Game.Race;
using BettingRace.Code.Infrastructure.DI;
using BettingRace.Code.UI;
using Cinemachine;

namespace BettingRace.Code.Services.Factories
{
    public interface IGameFactory : IService
    {
        CinemachineVirtualCamera VirtualCamera { get; }
        List<IHorse> Horses { get; }

        void CreateGameComponents(Action onCreated = null);
        IRace CreateRace(RaceProgressSliderGroup horseProgressSliders);
    }
}