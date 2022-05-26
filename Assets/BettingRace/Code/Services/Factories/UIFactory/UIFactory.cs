using System;
using System.Collections.Generic;
using BettingRace.Code.Data.StaticData;
using BettingRace.Code.Game.BetResult;
using BettingRace.Code.Infrastructure.DI;
using BettingRace.Code.Services.PersistentProgress;
using BettingRace.Code.Services.SaveLoad;
using BettingRace.Code.Services.Sound;
using BettingRace.Code.Services.StaticData;
using BettingRace.Code.UI;
using BettingRace.Code.UI.Bet;
using BettingRace.Code.UI.FinishRace;
using BettingRace.Code.UI.SelectHorseLayout;
using BettingRace.Code.UI.Settings;
using BettingRace.Code.UI.StartRace;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace BettingRace.Code.Services.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        public SelectHorseLayoutGroup SelectHorseLayoutGroup { get; private set; }
        public StartRacePanel StartRacePanel { get; private set; }
        public StartRaceCountdown StartRaceCountdown { get; private set; }
        public IBetModifier BetModifier { get; private set; }
        public ManageBetPanel ManageBetPanel { get; private set; }
        public ResetBalanceLabel ResetBalanceLabel { get; private set; }
        public FinishRacePanel FinishRacePanel { get; private set; }
        public FinishHorseLayoutGroup FinishedHorseList { get; private set; }
        public IBetResultCalculator BetResultCalculator { get; private set; }
        public RaceProgressSliderGroup HorseProgressSliders { get; private set; }
        public GameSettings GameSettings { get; private set; }

        public List<ISaveLoadProgress> ProgressUsers { get; } = new List<ISaveLoadProgress>();

        private readonly IStaticDataService _staticData;

        public UIFactory(IStaticDataService staticData) => _staticData = staticData;

        public void CreateUI(Action onCreated = null)
        {
            UIStaticData uiData = _staticData.GetUIData();
            BetStaticData betData = _staticData.GetBetData();
            RootUI rootUI = Object.Instantiate(uiData.RootUIPrefab).GetComponent<RootUI>();
            
            InitializeStartRaceComponents(rootUI);
            InitializeSelectHorseLayoutGroup(rootUI, uiData.SelectHorseElement ,uiData.BetSelectedSprite);
            InitializeBetPanel(rootUI, betData);
            InitializeFinishRacePanel(rootUI, uiData.FinishedHorseElement, betData, uiData.FinishSelectedSprite);
            InitializeRaceProgressSliders(rootUI, uiData.HorseProgressSlider);
            CreateSettings(rootUI.SettingsPanel.SoundSwitcher);
            
            onCreated?.Invoke();
        }

        private void CreateSettings(Switcher soundSwitcher)
        {
            GameSettings = new GameSettings(soundSwitcher,
                AllServices.Container.Single<SoundService>(),
                AllServices.Container.Single<ISaveLoadService>());
            RegisterProgressUser(GameSettings);
        }

        public void Cleanup() => ProgressUsers.Clear();

        private void InitializeStartRaceComponents(RootUI rootUI)
        {
            StartRacePanel = rootUI.StartRacePanel;
            StartRaceCountdown = rootUI.StartRaceCountdown;
        }
        
        private void InitializeSelectHorseLayoutGroup(RootUI rootUI, GameObject prefab, Sprite selectedSprite)
        {
            Transform horseLayoutTransform = rootUI.SelectHorseLayoutContent;
            List<SelectHorseLayoutElement> elements = InstantiateHorseLayoutElements<SelectHorseLayoutElement>(
                prefab, horseLayoutTransform, _staticData.GetHorses());
            SelectHorseLayoutGroup = new SelectHorseLayoutGroup(elements, StartRacePanel, selectedSprite);
        }

        private void InitializeBetPanel(RootUI rootUI, BetStaticData betData)
        {
            ManageBetPanel = rootUI.ManageBetPanel;
            ManageBetPanel.Construct(betData.BetValues);
            ResetBalanceLabel = rootUI.BetRootPanel.ResetBalanceLabel;
            BetModifier = new BetModifier(AllServices.Container.Single<ISaveLoadService>(), betData.MinBet);
            RegisterProgressUser(BetModifier);
        }

        private void InitializeFinishRacePanel(RootUI rootUI, GameObject prefab, BetStaticData betData, Sprite selectedSprite)
        {
            FinishRacePanel = rootUI.FinishRacePanel;
            FinishRacePanel.Hide();
            BetResultCalculator = new BetResultCalculator(
                AllServices.Container.Single<ISaveLoadService>(),
                AllServices.Container.Single<SoundService>(),
                FinishRacePanel, betData);
            RegisterProgressUser(BetResultCalculator);
            List<FinishHorseElement> elements = 
                InstantiateHorseLayoutElements<FinishHorseElement>(prefab, FinishRacePanel.FinishedHorseContent, _staticData.GetHorses());
            FinishedHorseList = new FinishHorseLayoutGroup(elements, selectedSprite);
        }

        private void InitializeRaceProgressSliders(RootUI rootUI, GameObject sliderPrefab)
        {
            HorseProgressSliders = rootUI.RaceProgressSliderGroup;
            HorseProgressSliders.Hide();
            InstantiateHorseProgressSliders(sliderPrefab, HorseProgressSliders.transform, HorseProgressSliders.HorseProgressSliders);
        }

        private void RegisterProgressUser(ISaveLoadProgress progressWriter) =>
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