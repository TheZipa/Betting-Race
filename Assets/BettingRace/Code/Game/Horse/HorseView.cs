using System;
using BettingRace.Code.Infrastructure;
using UnityEngine;

namespace BettingRace.Code.Game.Horse
{
    public class HorseView : MonoBehaviour, ICoroutineRunner
    {
        public event Action<Transform> OnViewUpdate;
        public HorseAnimator Animator;

        [SerializeField] private SpriteRenderer _horseSprite;

        private void Update() =>
            OnViewUpdate?.Invoke(transform);

        public void SetSprite(Sprite sprite, int sortingOrder)
        {
            _horseSprite.sprite = sprite;
            _horseSprite.sortingOrder = sortingOrder;
        }
    }
}