using UnityEngine;

namespace BettingRace.Code.Data.StaticData
{
    [CreateAssetMenu(fileName = "Horse Data", menuName = "Static Data/Horses/Horse Data")]
    public class HorseData : ScriptableObject
    {
        public Sprite View;
        public Color SliderColor;
        public string Name;
    }
}