using BettingRace.Code.Data.Sound;
using BettingRace.Code.Data.StaticData;
using BettingRace.Code.Infrastructure.DI;

namespace BettingRace.Code.Services.StaticDataProvider
{
    public interface IStaticDataProvider : IService
    {
        SoundData[] GetAllSoundData();
        HorseMovementStaticData GetHorseMovementData();
        HorseData[] GetAllHorseData();
        PositionStaticData GetPositionData();
        UIStaticData GetUIData();
        BetStaticData GetBetData();
    }
}