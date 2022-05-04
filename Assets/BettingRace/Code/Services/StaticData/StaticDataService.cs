using System.Collections.Generic;
using System.Linq;
using BettingRace.Code.Data.Constants;
using BettingRace.Code.Data.StaticData;
using UnityEngine;

namespace BettingRace.Code.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private List<HorseData> _horses;
        
        private HorseMovementStaticData _horseMovementData;
        private PositionStaticData _positionData;
        private UIStaticData _uiData;
        private BetStaticData _betData;
        
        public void LoadStaticData()
        {
            _horseMovementData = Resources.Load<HorseMovementStaticData>(PathConstants.HorseMovementData);
            _positionData = Resources.Load<PositionStaticData>(PathConstants.PositionData);
            _uiData = Resources.Load<UIStaticData>(PathConstants.UIData);
            _betData = Resources.Load<BetStaticData>(PathConstants.BetData);
            _horses = Resources.LoadAll<HorseData>(PathConstants.HorseData).ToList();
        }

        public PositionStaticData GetPositionData() => _positionData;

        public HorseMovementStaticData GetHorseMovementData() => _horseMovementData;
        
        public UIStaticData GetUIData() => _uiData;
        public BetStaticData GetBetData() => _betData;

        public List<HorseData> GetHorses() => _horses;
    }
}