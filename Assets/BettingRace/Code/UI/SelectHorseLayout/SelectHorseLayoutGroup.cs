using System;
using System.Collections.Generic;
using BettingRace.Code.UI.StartRace;
using UnityEngine;

namespace BettingRace.Code.UI.SelectHorseLayout
{
    public class SelectHorseLayoutGroup
    {
        public event Action<int> OnHorseSwitched;
        
        private readonly List<SelectHorseLayoutElement> _horseElements;
        private readonly StartRacePanel _startRacePanel;
        
        private readonly Sprite _selectedSprite;
        private readonly Sprite _standartSprite;
        
        private SelectHorseLayoutElement _selectedSelectHorse;

        public SelectHorseLayoutGroup(List<SelectHorseLayoutElement> elements, StartRacePanel startRacePanel, Sprite selectedSprite)
        {
            _horseElements = elements;
            _selectedSprite = selectedSprite;
            _standartSprite = elements[0].GetSprite();
            _startRacePanel = startRacePanel;
            
            SubscribeElements();
            SelectElement(elements[0]);
        }

        private void SubscribeElements()
        {
            foreach (SelectHorseLayoutElement horseElement in _horseElements)
                horseElement.OnSelect += SwitchSelectedElement;
        }

        private void UnsubscribeElements()
        {
            foreach (SelectHorseLayoutElement horseElement in _horseElements)
                horseElement.OnSelect -= SwitchSelectedElement;
        }
        
        private void DeselectElement() =>
            _selectedSelectHorse.SetSprite(_standartSprite);

        private void SelectElement(SelectHorseLayoutElement selectHorseElement)
        {
            _selectedSelectHorse = selectHorseElement;
            _selectedSelectHorse.SetSprite(_selectedSprite);
            _startRacePanel.SetSelectedHorseSprite(_selectedSelectHorse.GetCarSprite());
        }

        private void SwitchSelectedElement(SelectHorseLayoutElement newSelectedElement)
        {
            DeselectElement();
            SelectElement(newSelectedElement);
            OnHorseSwitched?.Invoke(_selectedSelectHorse.Id);
        }

        ~SelectHorseLayoutGroup() => UnsubscribeElements();
    }
}