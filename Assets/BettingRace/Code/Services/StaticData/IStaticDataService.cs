using System.Collections.Generic;
using BettingRace.Code.Data.Sound;
using BettingRace.Code.Data.StaticData;
using BettingRace.Code.Infrastructure.DI;
using UnityEngine;

namespace BettingRace.Code.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadStaticData();

        PositionStaticData GetPositionData();
        HorseMovementStaticData GetHorseMovementData();
        UIStaticData GetUIData();
        BetStaticData GetBetData();
        AudioClip GetSound(SoundType sound);

        List<HorseData> GetHorses();
    }
}