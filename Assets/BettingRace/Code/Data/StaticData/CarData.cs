using UnityEngine;

namespace BettingRace.Code.Data.StaticData
{
    [CreateAssetMenu(fileName = "Car Data", menuName = "Static Data/Cars/Car Data")]
    public class CarData : ScriptableObject
    {
        public Sprite View;
        public Color SliderColor;
        public string Name;
    }
}