using System;
using UnityEngine;

namespace BettingRace.Code.Game.Car
{
    public class Car : ICar
    {
        public event Action<float, int> OnMoved;
        public event Action<ICar> OnFinish;
        public int Id { get; }
        private static int _id;
        
        private readonly CarMovement _movement;
        private readonly CarView _view;

        public Car(CarMovement movement, CarView view)
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
            _movement.OnMoved += OnCarMoved;
            _movement.OnFinish += OnCarFinished;
        }

        private void UnsubscribeMovement()
        {
            _movement.OnMoved -= OnCarMoved;
            _movement.OnFinish -= OnCarFinished;
        }

        private void OnCarFinished() =>
            OnFinish?.Invoke(this);

        private void OnCarMoved(float position) =>
            OnMoved?.Invoke(position, Id);

        ~Car() => UnsubscribeMovement();
    }
}