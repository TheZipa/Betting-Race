using UnityEngine;

namespace BettingRace.Code.Game.Horse
{
    [RequireComponent(typeof(Animator))]
    public class HorseAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private readonly int _run = Animator.StringToHash("Run");
        private readonly int _stand = Animator.StringToHash("Stand");

        public void SetOverrideAnimationController(AnimatorOverrideController overrideController) =>
            _animator.runtimeAnimatorController = overrideController;

        public void PlayRunAnimation() => _animator.SetTrigger(_run);

        public void PlayStandAnimation() => _animator.SetTrigger(_stand);
    }
}