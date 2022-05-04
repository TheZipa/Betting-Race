using System;
using System.Collections.Generic;
using BettingRace.Code.Data.Constants;
using BettingRace.Code.Data.StaticData;
using BettingRace.Code.Game.Horse;
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
        public List<IHorse> Horses { get; }

        private readonly IStaticDataService _staticData;

        public GameFactory(IStaticDataService staticData)
        {
            _staticData = staticData;
            Horses = new List<IHorse>(4);
        }

        public void CreateGameComponents(Action onCreated = null)
        {
            CreateHorses();
            CreateFollowCamera();

            onCreated?.Invoke();
        }

        public IRace CreateRace(RaceProgressSliderGroup horseProgressSliders) =>
            new Race(Horses, VirtualCamera, horseProgressSliders);

        private void CreateHorses()
        {
            GameObject horsePrefab = Resources.Load<GameObject>(PathConstants.HorsePrefab);
            PositionStaticData positionData = _staticData.GetPositionData();
            HorseMovementStaticData horseMovementData = _staticData.GetHorseMovementData();
            List<HorseData> horsesData = _staticData.GetHorses();
            Horses.Clear();
            Horse.ResetId();

            for(int i = 0; i < horsesData.Count; i++)
            {
                HorseView horseView = InstantiateHorseView(i, horsePrefab, positionData);
                horseView.SetSprite(horsesData[i].View);

                InitializeHorse(horseMovementData, horseView, positionData.FinishDistance);
            }
        }

        private void InitializeHorse(HorseMovementStaticData horseMovementData, HorseView horseView, float finishDistance)
        {
            IHorseAccelerator accelerator =
                new HorseAccelerator(horseMovementData.AccelerationCurve, horseMovementData.AccelerationStep);
            HorseMovement horseMovement = 
                new HorseMovement(horseView, accelerator, horseMovementData.MinSpeed, horseMovementData.MaxSpeed, finishDistance);
            Horses.Add(new Horse(horseMovement, horseView));
        }

        private void CreateFollowCamera()
        {
            GameObject cameraPrefab = Resources.Load<GameObject>(PathConstants.Camera);
            GameObject camera = Object.Instantiate(cameraPrefab);
            VirtualCamera = camera.GetComponent<CinemachineVirtualCamera>();
        }

        private HorseView InstantiateHorseView(int index, GameObject horsePrefab, PositionStaticData positionData)
        {
            Vector3 spawnPoint = GetSpawnPoint(index, positionData);
            GameObject horse = Object.Instantiate(horsePrefab, spawnPoint, Quaternion.identity);
            HorseView horseView = horse.GetComponent<HorseView>();
            return horseView;
        }

        private Vector3 GetSpawnPoint(int index, PositionStaticData positionData)
        {
            Vector3 spawnPoint = positionData.HorseSpawnPoint;
            spawnPoint.y = positionData.SpawnOffset * index;
            return spawnPoint;
        }
    }
}