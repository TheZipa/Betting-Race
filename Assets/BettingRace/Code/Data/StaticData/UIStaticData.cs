using UnityEngine;

namespace BettingRace.Code.Data.StaticData
{
    [CreateAssetMenu(fileName = "UI Data", menuName = "Static Data/UI Data")]
    public class UIStaticData : ScriptableObject
    {
        public GameObject RootUIPrefab;
        public GameObject SelectCarElement;
        public GameObject FinishedCardElement;
        public GameObject CarProgressSlider;
        public Color CarSelectColor;
    }
}