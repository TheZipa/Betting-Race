using System.Collections.Generic;
using UnityEngine;

namespace BettingRace.Code.UI.FinishRace
{
    public class FinishHorseLayoutGroup
    {
        private readonly List<FinishHorseElement> _horses;
        private readonly Sprite _selectedSprite;
        private readonly Sprite _standartSprite;

        private FinishHorseElement _chosenHorse;

        public FinishHorseLayoutGroup(List<FinishHorseElement> horses, Sprite selectedSprite)
        {
            _horses = horses;
            _selectedSprite = selectedSprite;
            FinishHorseElement firstElement = _horses[0];
            _standartSprite = firstElement.GetSprite();
            _chosenHorse = firstElement;
            _chosenHorse.SetSprite(_selectedSprite);
        }

        public void SetChosenHorse(int horseId)
        {
            _chosenHorse.SetSprite(_standartSprite);
            _chosenHorse = _horses[horseId];
            _chosenHorse.SetSprite(_selectedSprite);
        }

        public void SetFinishedHorsePosition(int position, int horseId)
        {
            FinishHorseElement horseElement = _horses[horseId - 1];
            horseElement.transform.SetSiblingIndex(position - 1);
            horseElement.SetFinishIndex(position);
        }
    }
}