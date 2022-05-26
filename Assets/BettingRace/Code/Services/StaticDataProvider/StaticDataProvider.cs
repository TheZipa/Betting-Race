using BettingRace.Code.Data.Sound;
using BettingRace.Code.Data.StaticData;
using UnityEngine;

namespace BettingRace.Code.Services.StaticDataProvider
{
    public class StaticDataProvider : IStaticDataProvider
    {
        private const string SoundsPath = "StaticData/Sounds";
        private const string HorseMovementDataPath = "StaticData/Horse Movement Data";
        private const string HorseDataPath = "StaticData/Horses";
        private const string PositionDataPath = "StaticData/Position Data";
        private const string UIDataPath = "StaticData/UI Data";
        private const string BetDataPath = "StaticData/Bet Data";

        public SoundData[] GetAllSoundData() =>
            Resources.LoadAll<SoundData>(SoundsPath);

        public HorseMovementStaticData GetHorseMovementData() =>
            Resources.Load<HorseMovementStaticData>(HorseMovementDataPath);

        public HorseData[] GetAllHorseData() =>
            Resources.LoadAll<HorseData>(HorseDataPath);

        public PositionStaticData GetPositionData() =>
            Resources.Load<PositionStaticData>(PositionDataPath);

        public UIStaticData GetUIData() =>
            Resources.Load<UIStaticData>(UIDataPath);

        public BetStaticData GetBetData() =>
            Resources.Load<BetStaticData>(BetDataPath);
    }
}