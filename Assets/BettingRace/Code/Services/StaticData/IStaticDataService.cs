using System.Collections.Generic;
using BettingRace.Code.Data.StaticData;
using BettingRace.Code.Infrastructure.DI;

namespace BettingRace.Code.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadStaticData();

        PositionStaticData GetPositionData();
        CarMovementStaticData GetCarMovementData();
        UIStaticData GetUIData();
        BetStaticData GetBetData();

        List<CarData> GetCars();
    }
}