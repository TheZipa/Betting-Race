using System;
using BettingRace.Code.Infrastructure;
using UnityEngine;

namespace BettingRace.Code.Game.Horse
{
    public class HorseView : MonoBehaviour, ICoroutineRunner
    {
        public event Action<Transform> OnViewUpdate;

        [SerializeField] private SpriteRenderer _horseSprite;
        
        private void Update() =>
            OnViewUpdate?.Invoke(transform);

        public void SetSprite(Sprite carSprite) =>
            _horseSprite.sprite = carSprite;
    }
}