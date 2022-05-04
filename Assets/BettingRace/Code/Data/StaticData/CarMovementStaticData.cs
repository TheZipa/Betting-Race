using UnityEngine;

namespace BettingRace.Code.Data.StaticData
{
    [CreateAssetMenu(fileName = "Car Movement Data", menuName = "Static Data/Cars/Car Movement Data")]
    public class CarMovementStaticData : ScriptableObject
    {
        [Header("Acceleration")]
        public AnimationCurve AccelerationCurve;
        [Range(0.05f, 0.15f)]
        public float AccelerationStep;
        
        [Header("Speed")]
        [Range(5f, 10f)]
        public float MinSpeed;
        [Range(10f, 30f)]
        public float MaxSpeed;
    }
}