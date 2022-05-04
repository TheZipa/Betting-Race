using BettingRace.Code.Data;
using BettingRace.Code.Infrastructure.DI;
using BettingRace.Code.Services.Factories;
using BettingRace.Code.Services.PersistentProgress;
using BettingRace.Code.Services.SaveLoad;
using BettingRace.Code.Services.StaticData;

namespace BettingRace.Code.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        //private readonly LoadingCurtain _loadingCurtain;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            //_loadingCurtain = loadingCurtain;
            
            RegisterServices();
        }

        public void Enter()
        {
            //_loadingCurtain.Show();
            _stateMachine.Enter<LoadProgressState>();
        }

        public void Exit()
        {
        }
        
        private void RegisterServices()
        {
            RegisterStateMachine();
            RegisterPersistentProgress();
            RegisterStaticData();
            RegisterGameFactory();
            RegisterUIFactory();
            RegisterSaveLoad();
        }

        private void RegisterStateMachine() => 
            AllServices.Container.RegisterSingle<IGameStateMachine>(_stateMachine);

        private void RegisterPersistentProgress() =>
            AllServices.Container.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());

        private void RegisterStaticData() 
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadStaticData();
            AllServices.Container.RegisterSingle(staticDataService);
        }

        private void RegisterGameFactory() =>
            AllServices.Container.RegisterSingle<IGameFactory>(new GameFactory(
                AllServices.Container.Single<IStaticDataService>()));

        private void RegisterUIFactory() =>
            AllServices.Container.RegisterSingle<IUIFactory>(new UIFactory(
                AllServices.Container.Single<IStaticDataService>()));

        private void RegisterSaveLoad() =>
            AllServices.Container.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                AllServices.Container.Single<IPersistentProgressService>(), 
                AllServices.Container.Single<IUIFactory>()));
    }
}