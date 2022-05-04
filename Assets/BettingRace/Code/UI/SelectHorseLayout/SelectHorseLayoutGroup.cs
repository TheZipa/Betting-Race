using System;
using System.Collections.Generic;
using BettingRace.Code.UI.StartRace;
using UnityEngine;

namespace BettingRace.Code.UI.SelectHorseLayout
{
    public class SelectHorseLayoutGroup
    {
        public event Action<int> OnCarSwitched;
        
        private readonly List<SelectHorseLayoutElement> _horseElements;
        private readonly StartRacePanel _startRacePanel;
        
        private readonly Color _selectColor;
        private readonly Color _deselectColor;
        
        private SelectHorseLayoutElement _selectedSelectHorse;

        public SelectHorseLayoutGroup(List<SelectHorseLayoutElement> elements, StartRacePanel startRacePanel, Color selectColor)
        {
            _horseElements = elements;
            _selectColor = selectColor;
            _deselectColor = elements[0].GetColor();
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
            _selectedSelectHorse.SetColor(_deselectColor);

        private void SelectElement(SelectHorseLayoutElement selectHorseElement)
        {
            _selectedSelectHorse = selectHorseElement;
            _selectedSelectHorse.SetColor(_selectColor);
            _startRacePanel.SetSelectedHorseSprite(_selectedSelectHorse.GetCarSprite());
        }

        private void SwitchSelectedElement(SelectHorseLayoutElement newSelectedElement)
        {
            DeselectElement();
            SelectElement(newSelectedElement);
            OnCarSwitched?.Invoke(_selectedSelectHorse.Id);
        }

        ~SelectHorseLayoutGroup() => UnsubscribeElements();
    }
}