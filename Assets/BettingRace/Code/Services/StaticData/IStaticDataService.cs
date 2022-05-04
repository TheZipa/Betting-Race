using System.Collections.Generic;
using BettingRace.Code.Data.StaticData;
using BettingRace.Code.Infrastructure.DI;

namespace BettingRace.Code.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadStaticData();

        PositionStaticData GetPositionData();
        HorseMovementStaticData GetHorseMovementData();
        UIStaticData GetUIData();
        BetStaticData GetBetData();

        List<HorseData> GetHorses();
    }
}