using System;
using System.Collections.Generic;
using BettingRace.Code.Data;
using BettingRace.Code.Data.Sound;
using BettingRace.Code.Data.StaticData;
using BettingRace.Code.Game.Environment.BackgroundParallax;
using BettingRace.Code.Game.Horse;
using BettingRace.Code.Game.Race;
using BettingRace.Code.Services.AssetProvider;
using BettingRace.Code.Services.Sound;
using BettingRace.Code.Services.StaticData;
using BettingRace.Code.UI;
using Cinemachine;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BettingRace.Code.Services.Factories.GameFactory
{
    public class GameFactory : IGameFactory
    {
        public CinemachineVirtualCamera VirtualCamera { get; private set; }
        public BackgroundParallax BackgroundParallax { get; private set; }
        public List<IHorse> Horses { get; } = new List<IHorse>(4);

        private readonly IStaticDataService _staticData;
        private readonly IAssetProvider _assetProvider;
        private readonly SoundService _soundService;

        public GameFactory(IStaticDataService staticData, IAssetProvider assetProvider, SoundService soundService)
        {
            _staticData = staticData;
            _assetProvider = assetProvider;
            _soundService = soundService;
        }

        public void CreateGameComponents(Action onCreated = null)
        {
            PositionStaticData positionData = _staticData.GetPositionData();
            CreateHorses(positionData);
            CreateFollowCamera();
            CreateParallaxBackground();
            CreateFinishTribunes(positionData.FinishDistance);
            CreateStartFinishLines(positionData);
            InitializeSoundEmitter();

            onCreated?.Invoke();
        }

        public IRace CreateRace(RaceProgressSliderGroup horseProgressSliders) =>
            new Race(Horses, VirtualCamera, horseProgressSliders, _soundService);

        private void CreateHorses(PositionStaticData positionData)
        {
            GameObject horsePrefab = _assetProvider.GetHorsePrefab();
            HorseMovementStaticData horseMovementData = _staticData.GetHorseMovementData();
            List<HorseData> horsesData = _staticData.GetHorses();
            Horses.Clear();
            Horse.ResetId();

            for(int i = 0; i < horsesData.Count; i++)
            {
                HorseView horseView = InstantiateHorseView(i, horsePrefab, positionData);
                horseView.Animator.SetOverrideAnimationController(horsesData[i].AnimationOverride);
                horseView.SetSprite(horsesData[i].View, i);

                InitializeHorse(horseMovementData, horseView, positionData.FinishDistance);
            }
        }

        private void CreateParallaxBackground()
        {
            GameObject backgroundPrefab = _assetProvider.GetBackgroundParallaxPrefab();
            GameObject parallaxBackground = Object.Instantiate(backgroundPrefab);
            ParallaxLayer[] parallaxLayers = parallaxBackground.GetComponentsInChildren<ParallaxLayer>();
            BackgroundParallax = new BackgroundParallax(parallaxLayers, Horses);
        }

        private void CreateFinishTribunes(float finishDistance)
        {
            GameObject tribunesPrefab = _assetProvider.GetTribunesPrefab();
            GameObject finishTribunes = Object.Instantiate(tribunesPrefab);
            finishTribunes.SetPositionX(finishDistance);
        }

        private void CreateStartFinishLines(PositionStaticData positionData)
        {
            GameObject linePrefab = _assetProvider.GetStartFinishLinePrefab();
            
            GameObject startLine = Object.Instantiate(linePrefab);
            startLine.SetPositionX(positionData.HorseSpawnPoint.x + 1f);

            GameObject finishLine = Object.Instantiate(linePrefab);
            finishLine.SetPositionX(positionData.FinishDistance);
        }

        private void CreateFollowCamera()
        {
            GameObject cameraPrefab = _assetProvider.GetCameraPrefab();
            GameObject camera = Object.Instantiate(cameraPrefab);
            VirtualCamera = camera.GetComponent<CinemachineVirtualCamera>();
        }

        private void InitializeHorse(HorseMovementStaticData horseMovementData, HorseView horseView, float finishDistance)
        {
            IHorseAccelerator accelerator =
                new HorseAccelerator(horseMovementData.AccelerationCurve, horseMovementData.AccelerationStep);
            HorseMovement horseMovement = 
                new HorseMovement(horseView, accelerator, horseMovementData.MinSpeed, horseMovementData.MaxSpeed, finishDistance);
            Horses.Add(new Horse(horseMovement, horseView));
        }

        private void InitializeSoundEmitter()
        {
            _soundService.Construct(_staticData);
            _soundService.CreateAudioGroup(SoundType.HorseRun, Horses.Count);
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
            spawnPoint.y -= positionData.SpawnOffset * index;
            return spawnPoint;
        }
    }
}