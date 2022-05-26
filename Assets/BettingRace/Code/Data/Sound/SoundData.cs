using UnityEngine;

namespace BettingRace.Code.Data.Sound
{
    [CreateAssetMenu(fileName = "Sound", menuName = "Static Data/Sound")]
    public class SoundData : ScriptableObject
    {
        public SoundType Type;
        public AudioClip Sound;
    }
}