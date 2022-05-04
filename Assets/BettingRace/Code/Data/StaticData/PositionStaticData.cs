using UnityEngine;

namespace BettingRace.Code.Data.StaticData
{
    [CreateAssetMenu(fileName = "Position Data", menuName = "Static Data/Position Data")]
    public class PositionStaticData : ScriptableObject
    {
        public Vector3 CarSpawnPoint;
        public float FinishDistance;
        public float SpawnOffset;
    }
}