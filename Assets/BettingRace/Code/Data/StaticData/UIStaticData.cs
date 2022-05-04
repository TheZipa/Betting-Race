using UnityEngine;

namespace BettingRace.Code.Data.StaticData
{
    [CreateAssetMenu(fileName = "UI Data", menuName = "Static Data/UI Data")]
    public class UIStaticData : ScriptableObject
    {
        public GameObject RootUIPrefab;
        public GameObject SelectHorseElement;
        public GameObject FinishedHorseElement;
        public GameObject HorseProgressSlider;
        public Color SelectColor;
    }
}