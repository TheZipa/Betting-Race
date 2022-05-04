using System.Collections.Generic;
using System.Linq;
using BettingRace.Code.Data.Constants;
using BettingRace.Code.Data.StaticData;
using UnityEngine;

namespace BettingRace.Code.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private List<CarData> _cars;
        
        private CarMovementStaticData _carMovementData;
        private PositionStaticData _positionData;
        private UIStaticData _uiData;
        private BetStaticData _betData;
        
        public void LoadStaticData()
        {
            _carMovementData = Resources.Load<CarMovementStaticData>(PathConstants.CarMovementData);
            _positionData = Resources.Load<PositionStaticData>(PathConstants.PositionData);
            _uiData = Resources.Load<UIStaticData>(PathConstants.UIData);
            _betData = Resources.Load<BetStaticData>(PathConstants.BetData);
            _cars = Resources.LoadAll<CarData>(PathConstants.CarData).ToList();
        }

        public PositionStaticData GetPositionData() => _positionData;

        public CarMovementStaticData GetCarMovementData() => _carMovementData;
        
        public UIStaticData GetUIData() => _uiData;
        public BetStaticData GetBetData() => _betData;

        public List<CarData> GetCars() => _cars;
    }
}