using System;
using UnityEngine;

namespace BettingRace.Code.Game.Horse
{
    public class Horse : IHorse
    {
        public event Action<float, int> OnMoved;
        public event Action<IHorse> OnFinish;
        public int Id { get; }
        private static int _id;
        
        private readonly HorseMovement _movement;
        private readonly HorseView _view;

        public Horse(HorseMovement movement, HorseView view)
        {
            _movement = movement;
            _view = view;

            Id = ++_id;
            SubscribeMovement();
        }

        public void Start() => _movement.Start();

        public Transform GetTransform() => _view.transform;

        public static void ResetId() => _id = 0;

        private void SubscribeMovement()
        {
            _movement.OnMoved += OnHorseMoved;
            _movement.OnFinish += OnHorseFinished;
        }

        private void UnsubscribeMovement()
        {
            _movement.OnMoved -= OnHorseMoved;
            _movement.OnFinish -= OnHorseFinished;
        }

        private void OnHorseFinished() =>
            OnFinish?.Invoke(this);

        private void OnHorseMoved(float position) =>
            OnMoved?.Invoke(position, Id);

        ~Horse() => UnsubscribeMovement();
    }
}