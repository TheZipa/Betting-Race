using UnityEngine;

namespace BettingRace.Code.Game.Environment.BackgroundParallax
{
    public class ParallaxLayer : MonoBehaviour
    {
        [Range(-1f, 1f)]
        [SerializeField] private float _parallaxFactor;
        private Transform _following;
        private Vector3 _previousPosition;

        public void SetFollowing(Transform toFollow, Vector3 previousPosition)
        {
            _following = toFollow;
            _previousPosition = previousPosition;
        }

        private void Update()
        {
            if (_following == null) return;
            
            Vector3 parallaxDelta = GetParallaxDelta();
            _previousPosition = _following.position;
            transform.position += parallaxDelta * _parallaxFactor;
        }

        private Vector3 GetParallaxDelta()
        {
            Vector3 delta = _following.position - _previousPosition;
            delta.y = 0;
            return delta;
        }
    }
}