using System;
using System.Collections.Generic;
using BettingRace.Code.Game.Car;
using BettingRace.Code.Game.Race;
using BettingRace.Code.Infrastructure.DI;
using BettingRace.Code.UI;
using Cinemachine;

namespace BettingRace.Code.Services.Factories
{
    public interface IGameFactory : IService
    {
        CinemachineVirtualCamera VirtualCamera { get; }
        List<ICar> Cars { get; }

        void CreateGameComponents(Action onCreated = null);
        IRace CreateRace(RaceProgressSliderGroup carProgressSliders);
    }
}