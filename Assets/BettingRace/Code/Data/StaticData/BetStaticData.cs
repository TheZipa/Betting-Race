using UnityEngine;

namespace BettingRace.Code.Data.StaticData
{
    [CreateAssetMenu(fileName = "Bet Data", menuName = "Static Data/Bet Data")]
    public class BetStaticData : ScriptableObject
    {
        [Header("Bet Modifier")]
        public int[] BetValues;
        public int MinBet;

        [Header("Bet Result")] 
        public Color WinTextColor;
        public Color LoseTextColor;
        public int BetFactor;
    }
}