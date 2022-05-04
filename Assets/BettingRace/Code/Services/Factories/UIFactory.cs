using System;
using System.Collections.Generic;
using BettingRace.Code.Data.StaticData;
using BettingRace.Code.Game.BetResult;
using BettingRace.Code.Infrastructure.DI;
using BettingRace.Code.Services.PersistentProgress;
using BettingRace.Code.Services.SaveLoad;
using BettingRace.Code.Services.StaticData;
using BettingRace.Code.UI;
using BettingRace.Code.UI.Bet;
using BettingRace.Code.UI.FinishRace;
using BettingRace.Code.UI.SelectCarLayout;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace BettingRace.Code.Services.Factories
{
    public class UIFactory : IUIFactory
    {
        public SelectCarLayoutGroup SelectCarLayoutGroup { get; private set; }
        public StartRacePanel StartRacePanel { get; private set; }
        public IBetModifier BetModifier { get; private set; }
        public BetPanel BetPanel { get; private set; }
        public ResetBalanceLabel ResetBalanceLabel { get; private set; }
        public FinishRacePanel FinishRacePanel { get; private set; }
        public FinishCarLayoutGroup FinishedCarList { get; private set; }
        public IBetResultCalculator BetResultCalculator { get; private set; }
        public RaceProgressSliderGroup CarProgressSliders { get; private set; }

        public List<ISavedProgress> ProgressUsers { get; } = new List<ISavedProgress>();

        private readonly IStaticDataService _staticData;

        public UIFactory(IStaticDataService staticData) => _staticData = staticData;

        public void CreateUI(Action onCreated = null)
        {
            UIStaticData uiData = _staticData.GetUIData();
            BetStaticData betData = _staticData.GetBetData();
            GameObject rootUI = Object.Instantiate(uiData.RootUIPrefab);
            
            InitializeSelectCarLayoutGroup(rootUI, uiData.SelectCarElement ,uiData.CarSelectColor);
            InitializeBetPanel(rootUI, betData);
            InitializeFinishRacePanel(rootUI, uiData.FinishedCardElement, betData, uiData.CarSelectColor);
            InitializeRaceProgressSliders(rootUI, uiData.CarProgressSlider);

            onCreated?.Invoke();
        }

        public void Cleanup() => ProgressUsers.Clear();

        private void InitializeSelectCarLayoutGroup(GameObject rootUI, GameObject prefab, Color carSelectColor)
        {
            Transform carLayoutTransform = rootUI.GetComponentInChildren<SelectCarLayoutParent>().transform;
            StartRacePanel = rootUI.GetComponentInChildren<StartRacePanel>();
            List<SelectCarLayoutElement> elements = InstantiateCarLayoutElements<SelectCarLayoutElement>(
                prefab, carLayoutTransform, _staticData.GetCars());
            SelectCarLayoutGroup = new SelectCarLayoutGroup(elements, StartRacePanel, carSelectColor);
        }

        private void InitializeBetPanel(GameObject rootUI, BetStaticData betData)
        {
            BetPanel = rootUI.GetComponentInChildren<BetPanel>();
            BetPanel.Construct(betData.BetValues);
            ResetBalanceLabel = rootUI.GetComponentInChildren<BetRootUI>().ResetBalanceLabel;
            BetModifier = new BetModifier(AllServices.Container.Single<ISaveLoadService>(), betData.MinBet);
            RegisterProgressUser(BetModifier);
        }

        private void InitializeFinishRacePanel(GameObject rootUI, GameObject prefab, BetStaticData betData, Color selectedColor)
        {
            FinishRacePanel = rootUI.GetComponentInChildren<FinishRacePanel>();
            FinishRacePanel.Hide();
            BetResultCalculator = new BetResultCalculator(AllServices.Container.Single<ISaveLoadService>(),
                FinishRacePanel, betData);
            RegisterProgressUser(BetResultCalculator);
            List<FinishCarElement> carElements = 
                InstantiateCarLayoutElements<FinishCarElement>(prefab, FinishRacePanel.FinishedCarParent, _staticData.GetCars());
            FinishedCarList = new FinishCarLayoutGroup(carElements, selectedColor);
        }

        private void InitializeRaceProgressSliders(GameObject rootUI, GameObject sliderPrefab)
        {
            CarProgressSliders = rootUI.GetComponentInChildren<RaceProgressSliderGroup>();
            CarProgressSliders.Hide();
            InstantiateCarProgressSliders(sliderPrefab, CarProgressSliders.transform, CarProgressSliders.CarProgressSliders);
        }

        private void RegisterProgressUser(ISavedProgress progressWriter) =>
            ProgressUsers.Add(progressWriter);

        private List<TElement> InstantiateCarLayoutElements<TElement>(GameObject prefab, Transform carLayoutTransform,
            List<CarData> cars) where TElement : UI.ILayoutElement
        {
            List<TElement> carElements = new List<TElement>(4);
            
            for (int i = 0; i < cars.Count; i++)
            {
                GameObject car = Object.Instantiate(prefab, carLayoutTransform);
                TElement layoutElement = car.GetComponent<TElement>();
                layoutElement.Construct(cars[i].View, cars[i].Name, i);
                carElements.Add(layoutElement);
            }

            return carElements;
        }

        private void InstantiateCarProgressSliders(GameObject sliderPrefab, Transform parent, List<Slider> sliderList)
        {
            PositionStaticData positionData = _staticData.GetPositionData();
            
            foreach (CarData car in _staticData.GetCars())
            {
                Slider carSlider = Object.Instantiate(sliderPrefab, parent).GetComponent<Slider>();
                carSlider.handleRect.GetComponent<Image>().color = car.SliderColor;
                carSlider.minValue = positionData.CarSpawnPoint.x;
                carSlider.maxValue = positionData.FinishDistance;
                sliderList.Add(carSlider);
            }
        }
    }
}