using System.Collections.Generic;
using UnityEngine;

namespace BettingRace.Code.UI.FinishRace
{
    public class FinishCarLayoutGroup
    {
        private readonly List<FinishCarElement> _cars;
        private readonly Color _selectColor;
        private readonly Color _defaultColor;

        private FinishCarElement _chosenCar;

        public FinishCarLayoutGroup(List<FinishCarElement> cars, Color selectColor)
        {
            _cars = cars;
            _selectColor = selectColor;
            FinishCarElement firstElement = _cars[0];
            _defaultColor = firstElement.GetColor();
            _chosenCar = firstElement;
            _chosenCar.SetColor(_selectColor);
        }

        public void SetChosenCar(int carId)
        {
            _chosenCar.SetColor(_defaultColor);
            _chosenCar = _cars[carId];
            _chosenCar.SetColor(_selectColor);
        }

        public void SetFinishedCarPosition(int position, int carId)
        {
            FinishCarElement carElement = _cars[carId - 1];
            carElement.transform.SetSiblingIndex(position - 1);
            carElement.SetFinishIndex(position);
        }
    }
}