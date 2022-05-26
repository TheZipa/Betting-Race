using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BettingRace.Code.UI
{
    public class RaceProgressSliderGroup : MonoBehaviour
    {
        [HideInInspector]
        public List<Slider> HorseProgressSliders = new List<Slider>(4);

        public void RefreshSliderValue(float position, int id) =>
            HorseProgressSliders[id - 1].value = position;

        public void Hide() => gameObject.SetActive(false);

        public void Show() => gameObject.SetActive(true);
    }
}