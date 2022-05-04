using System.Collections.Generic;
using UnityEngine;

namespace BettingRace.Code.UI.FinishRace
{
    public class FinishHorseLayoutGroup
    {
        private readonly List<FinishHorseElement> _horses;
        private readonly Color _selectColor;
        private readonly Color _defaultColor;

        private FinishHorseElement _chosenHorse;

        public FinishHorseLayoutGroup(List<FinishHorseElement> horses, Color selectColor)
        {
            _horses = horses;
            _selectColor = selectColor;
            FinishHorseElement firstElement = _horses[0];
            _defaultColor = firstElement.GetColor();
            _chosenHorse = firstElement;
            _chosenHorse.SetColor(_selectColor);
        }

        public void SetChosenHorse(int horseId)
        {
            _chosenHorse.SetColor(_defaultColor);
            _chosenHorse = _horses[horseId];
            _chosenHorse.SetColor(_selectColor);
        }

        public void SetFinishedHorsePosition(int position, int horseId)
        {
            FinishHorseElement horseElement = _horses[horseId - 1];
            horseElement.transform.SetSiblingIndex(position - 1);
            horseElement.SetFinishIndex(position);
        }
    }
}