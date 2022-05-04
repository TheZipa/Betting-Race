using System;
using System.Collections.Generic;
using UnityEngine;

namespace BettingRace.Code.UI.SelectCarLayout
{
    public class SelectCarLayoutGroup
    {
        public event Action<int> OnCarSwitched;
        
        private readonly List<SelectCarLayoutElement> _carElements;
        private readonly StartRacePanel _startRacePanel;
        
        private readonly Color _selectColor;
        private readonly Color _deselectColor;
        
        private SelectCarLayoutElement _selectedSelectCar;

        public SelectCarLayoutGroup(List<SelectCarLayoutElement> elements, StartRacePanel startRacePanel, Color selectColor)
        {
            _carElements = elements;
            _selectColor = selectColor;
            _deselectColor = elements[0].GetColor();
            _startRacePanel = startRacePanel;
            
            SubscribeElements();
            SelectCarElement(elements[0]);
        }

        private void SubscribeElements()
        {
            foreach (SelectCarLayoutElement carElement in _carElements)
                carElement.OnSelect += SwitchSelectedCarElement;
        }

        private void UnsubscribeElements()
        {
            foreach (SelectCarLayoutElement carElement in _carElements)
                carElement.OnSelect -= SwitchSelectedCarElement;
        }
        
        private void DeselectCarElement() =>
            _selectedSelectCar.SetColor(_deselectColor);

        private void SelectCarElement(SelectCarLayoutElement selectCarElement)
        {
            _selectedSelectCar = selectCarElement;
            _selectedSelectCar.SetColor(_selectColor);
            _startRacePanel.SetSelectedCarSprite(_selectedSelectCar.GetCarSprite());
        }

        private void SwitchSelectedCarElement(SelectCarLayoutElement newSelectedElement)
        {
            DeselectCarElement();
            SelectCarElement(newSelectedElement);
            OnCarSwitched?.Invoke(_selectedSelectCar.Id);
        }

        ~SelectCarLayoutGroup() => UnsubscribeElements();
    }
}