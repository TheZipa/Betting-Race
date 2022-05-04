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
using BettingRace.Code.UI.SelectHorseLayout;
using BettingRace.Code.UI.StartRace;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace BettingRace.Code.Services.Factories
{
    public class UIFactory : IUIFactory
    {
        public SelectHorseLayoutGroup SelectHorseLayoutGroup { get; private set; }
        public StartRacePanel StartRacePanel { get; private set; }
        public StartRaceCountdown StartRaceCountdown { get; private set; }
        public IBetModifier BetModifier { get; private set; }
        public BetPanel BetPanel { get; private set; }
        public ResetBalanceLabel ResetBalanceLabel { get; private set; }
        public FinishRacePanel FinishRacePanel { get; private set; }
        public FinishHorseLayoutGroup FinishedHorseList { get; private set; }
        public IBetResultCalculator BetResultCalculator { get; private set; }
        public RaceProgressSliderGroup HorseProgressSliders { get; private set; }

        public List<ISavedProgress> ProgressUsers { get; } = new List<ISavedProgress>();

        private readonly IStaticDataService _staticData;

        public UIFactory(IStaticDataService staticData) => _staticData = staticData;

        public void CreateUI(Action onCreated = null)
        {
            UIStaticData uiData = _staticData.GetUIData();
            BetStaticData betData = _staticData.GetBetData();
            GameObject rootUI = Object.Instantiate(uiData.RootUIPrefab);
            
            InitializeStartRaceComponents(rootUI);
            InitializeSelectHorseLayoutGroup(rootUI, uiData.SelectHorseElement ,uiData.SelectColor);
            InitializeBetPanel(rootUI, betData);
            InitializeFinishRacePanel(rootUI, uiData.FinishedHorseElement, betData, uiData.SelectColor);
            InitializeRaceProgressSliders(rootUI, uiData.HorseProgressSlider);

            onCreated?.Invoke();
        }

        public void Cleanup() => ProgressUsers.Clear();

        private void InitializeStartRaceComponents(GameObject rootUI)
        {
            StartRacePanel = rootUI.GetComponentInChildren<StartRacePanel>();
            StartRaceCountdown = rootUI.GetComponentInChildren<StartRaceCountdown>();
        }
        
        private void InitializeSelectHorseLayoutGroup(GameObject rootUI, GameObject prefab, Color selectColor)
        {
            Transform horseLayoutTransform = rootUI.GetComponentInChildren<SelectHorseLayoutParent>().transform;
            List<SelectHorseLayoutElement> elements = InstantiateHorseLayoutElements<SelectHorseLayoutElement>(
                prefab, horseLayoutTransform, _staticData.GetHorses());
            SelectHorseLayoutGroup = new SelectHorseLayoutGroup(elements, StartRacePanel, selectColor);
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
            List<FinishHorseElement> elements = 
                InstantiateHorseLayoutElements<FinishHorseElement>(prefab, FinishRacePanel.FinishedHorseContent, _staticData.GetHorses());
            FinishedHorseList = new FinishHorseLayoutGroup(elements, selectedColor);
        }

        private void InitializeRaceProgressSliders(GameObject rootUI, GameObject sliderPrefab)
        {
            HorseProgressSliders = rootUI.GetComponentInChildren<RaceProgressSliderGroup>();
            HorseProgressSliders.Hide();
            InstantiateHorseProgressSliders(sliderPrefab, HorseProgressSliders.transform, HorseProgressSliders.HorseProgressSliders);
        }

        private void RegisterProgressUser(ISavedProgress progressWriter) =>
            ProgressUsers.Add(progressWriter);

        private List<TElement> InstantiateHorseLayoutElements<TElement>(GameObject prefab, Transform horseLayoutTransform,
            List<HorseData> horses) where TElement : UI.ILayoutElement
        {
            List<TElement> elements = new List<TElement>(4);
            
            for (int i = 0; i < horses.Count; i++)
            {
                GameObject horse = Object.Instantiate(prefab, horseLayoutTransform);
                TElement layoutElement = horse.GetComponent<TElement>();
                layoutElement.Construct(horses[i].View, horses[i].Name, i);
                elements.Add(layoutElement);
            }

            return elements;
        }

        private void InstantiateHorseProgressSliders(GameObject sliderPrefab, Transform parent, List<Slider> sliderList)
        {
            PositionStaticData positionData = _staticData.GetPositionData();
            
            foreach (HorseData horse in _staticData.GetHorses())
            {
                Slider horseProgressSlider = Object.Instantiate(sliderPrefab, parent).GetComponent<Slider>();
                horseProgressSlider.handleRect.GetComponent<Image>().color = horse.SliderColor;
                horseProgressSlider.minValue = positionData.HorseSpawnPoint.x;
                horseProgressSlider.maxValue = positionData.FinishDistance;
                sliderList.Add(horseProgressSlider);
            }
        }
    }
}