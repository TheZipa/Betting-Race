using System.Collections.Generic;
using System.Linq;
using BettingRace.Code.Data.Sound;
using BettingRace.Code.Data.StaticData;
using BettingRace.Code.Services.StaticDataProvider;
using UnityEngine;

namespace BettingRace.Code.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<SoundType, SoundData> _sounds;
        private List<HorseData> _horses;
        
        private HorseMovementStaticData _horseMovementData;
        private PositionStaticData _positionData;
        private UIStaticData _uiData;
        private BetStaticData _betData;

        private readonly IStaticDataProvider _dataProvider;

        public StaticDataService(IStaticDataProvider dataProvider) =>
            _dataProvider = dataProvider;
        
        public void LoadStaticData()
        {
            LoadSounds();
            _horseMovementData = _dataProvider.GetHorseMovementData();
            _positionData = _dataProvider.GetPositionData();
            _uiData = _dataProvider.GetUIData();
            _betData = _dataProvider.GetBetData();
            _horses = _dataProvider.GetAllHorseData().ToList();
        }

        public PositionStaticData GetPositionData() => _positionData;
        
        public HorseMovementStaticData GetHorseMovementData() => _horseMovementData;
        
        public UIStaticData GetUIData() => _uiData;
        
        public BetStaticData GetBetData() => _betData;
        
        public AudioClip GetSound(SoundType sound) => _sounds[sound].Sound;

        public List<HorseData> GetHorses() => _horses;

        private void LoadSounds() =>
            _sounds = _dataProvider.GetAllSoundData().ToDictionary(s => s.Type);
    }
}