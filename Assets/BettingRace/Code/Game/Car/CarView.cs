using System;
using BettingRace.Code.Infrastructure;
using UnityEngine;

namespace BettingRace.Code.Game.Car
{
    public class CarView : MonoBehaviour, ICoroutineRunner
    {
        public event Action<Transform> OnViewUpdate;

        [SerializeField] private SpriteRenderer _carSprite;
        
        private void Update() =>
            OnViewUpdate?.Invoke(transform);

        public void SetCarSprite(Sprite carSprite) =>
            _carSprite.sprite = carSprite;
    }
}