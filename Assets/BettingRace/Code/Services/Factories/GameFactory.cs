using System;
using System.Collections.Generic;
using BettingRace.Code.Data.Constants;
using BettingRace.Code.Data.StaticData;
using BettingRace.Code.Game.Car;
using BettingRace.Code.Game.Race;
using BettingRace.Code.Services.StaticData;
using BettingRace.Code.UI;
using Cinemachine;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BettingRace.Code.Services.Factories
{
    public class GameFactory : IGameFactory
    {
        public CinemachineVirtualCamera VirtualCamera { get; private set; }
        public List<ICar> Cars { get; }

        private readonly IStaticDataService _staticData;

        public GameFactory(IStaticDataService staticData)
        {
            _staticData = staticData;
            Cars = new List<ICar>(4);
        }

        public void CreateGameComponents(Action onCreated = null)
        {
            CreateCars();
            CreateFollowCamera();

            onCreated?.Invoke();
        }

        public IRace CreateRace(RaceProgressSliderGroup carProgressSliders) =>
            new Race(Cars, VirtualCamera, carProgressSliders);

        private void CreateCars()
        {
            GameObject carPrefab = Resources.Load<GameObject>(PathConstants.CarPrefab);
            PositionStaticData positionData = _staticData.GetPositionData();
            CarMovementStaticData carMovementData = _staticData.GetCarMovementData();
            List<CarData> carsData = _staticData.GetCars();
            Cars.Clear();
            Car.ResetId();

            for(int i = 0; i < carsData.Count; i++)
            {
                CarView carView = InstantiateCarView(i, carPrefab, positionData);
                carView.SetCarSprite(carsData[i].View);

                InitializeCar(carMovementData, carView, positionData.FinishDistance);
            }
        }

        private void InitializeCar(CarMovementStaticData carMovementData, CarView carView, float finishDistance)
        {
            ICarAccelerator accelerator =
                new CarAccelerator(carMovementData.AccelerationCurve, carMovementData.AccelerationStep);
            CarMovement carMovement = 
                new CarMovement(carView, accelerator, carMovementData.MinSpeed, carMovementData.MaxSpeed, finishDistance);
            Cars.Add(new Car(carMovement, carView));
        }

        private void CreateFollowCamera()
        {
            GameObject cameraPrefab = Resources.Load<GameObject>(PathConstants.Camera);
            GameObject camera = Object.Instantiate(cameraPrefab);
            VirtualCamera = camera.GetComponent<CinemachineVirtualCamera>();
        }

        private CarView InstantiateCarView(int carIndex, GameObject carPrefab, PositionStaticData positionData)
        {
            Vector3 spawnPoint = GetSpawnPoint(carIndex, positionData);
            GameObject car = Object.Instantiate(carPrefab, spawnPoint, Quaternion.identity);
            CarView carView = car.GetComponent<CarView>();
            return carView;
        }

        private Vector3 GetSpawnPoint(int carIndex, PositionStaticData positionData)
        {
            Vector3 spawnPoint = positionData.CarSpawnPoint;
            spawnPoint.y = positionData.SpawnOffset * carIndex;
            return spawnPoint;
        }
    }
}