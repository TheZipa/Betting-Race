using BettingRace.Code.Infrastructure.DI;
using BettingRace.Code.Services.AssetProvider;
using BettingRace.Code.Services.Factories.GameFactory;
using BettingRace.Code.Services.Factories.UIFactory;
using BettingRace.Code.Services.PersistentProgress;
using BettingRace.Code.Services.SaveLoad;
using BettingRace.Code.Services.Sound;
using BettingRace.Code.Services.StaticData;
using BettingRace.Code.Services.StaticDataProvider;
using UnityEngine;

namespace BettingRace.Code.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SoundService _soundService;

        public BootstrapState(GameStateMachine stateMachine, SoundService soundService)
        {
            _stateMachine = stateMachine;
            _soundService = soundService;

            RegisterServices();
            SetLandscapeOrientation();
        }

        public void Enter() =>
            _stateMachine.Enter<LoadProgressState>();

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            RegisterStateMachine();
            RegisterAssetProvider();
            RegisterDataProvider();
            RegisterSounds();
            RegisterPersistentProgress();
            RegisterStaticData();
            RegisterUIFactory();
            RegisterGameFactory();
            RegisterSaveLoad();
        }

        private void SetLandscapeOrientation()
        {
            Screen.orientation = ScreenOrientation.Landscape;
            Screen.orientation = ScreenOrientation.AutoRotation;
        
            Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = false;
            Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = true;

        }

        private void RegisterStateMachine() => 
            AllServices.Container.RegisterSingle<IGameStateMachine>(_stateMachine);

        private void RegisterDataProvider() =>
            AllServices.Container.RegisterSingle<IStaticDataProvider>(new StaticDataProvider());
        
        private void RegisterAssetProvider() =>
            AllServices.Container.RegisterSingle<IAssetProvider>(new AssetProvider());

        private void RegisterSounds() =>
            AllServices.Container.RegisterSingle(_soundService);
        
        private void RegisterPersistentProgress() =>
            AllServices.Container.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());

        private void RegisterStaticData() 
        {
            IStaticDataService staticDataService = new StaticDataService(
                AllServices.Container.Single<IStaticDataProvider>());
            staticDataService.LoadStaticData();
            AllServices.Container.RegisterSingle(staticDataService);
        }

        private void RegisterUIFactory() =>
            AllServices.Container.RegisterSingle<IUIFactory>(new UIFactory(
                AllServices.Container.Single<IStaticDataService>()));

        private void RegisterGameFactory() =>
            AllServices.Container.RegisterSingle<IGameFactory>(new GameFactory(
                AllServices.Container.Single<IStaticDataService>(), 
                AllServices.Container.Single<IAssetProvider>(), _soundService));

        private void RegisterSaveLoad() =>
            AllServices.Container.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                AllServices.Container.Single<IPersistentProgressService>()));
    }
}