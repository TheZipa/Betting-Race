using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BettingRace.Code.Game.Car
{
    public class CarMovement
    {
        public event Action OnFinish;
        public event Action<float> OnMoved;

        private readonly CarView _carView;
        private readonly ICarAccelerator _accelerator;
        private readonly float _minSpeed;
        private readonly float _maxSpeed;
        private readonly float _finishDistance;

        private Vector3 _currentVelocity;
        private float _currentSpeed;

        private bool _isMoving;
        private bool _isFinished;

        public CarMovement(CarView carView, ICarAccelerator accelerator, float minSpeed, float maxSpeed, float finishDistance)
        {
            _carView = carView;
            _accelerator = accelerator;
            _minSpeed = minSpeed;
            _maxSpeed = maxSpeed;
            _finishDistance = finishDistance;

            SubscribeView();
            SetRandomSpeed();
        }

        public void Start()
        {
            _accelerator.StartAccelerate(_carView, () => _carView.StartCoroutine(RandomizeVelocity()));
            _isMoving = true;
        }

        private void SubscribeView() => _carView.OnViewUpdate += Move;

        private void Move(Transform transform)
        {
            if (_isMoving == false) return;
            
            if(!_isFinished && transform.position.x >= _finishDistance) Finish();

            Vector3 carPosition = transform.position;
            OnMoved?.Invoke(carPosition.x);
            CalculateCurrentVelocity();
            carPosition += _currentVelocity * Time.deltaTime;
            transform.position = carPosition;
        }

        private IEnumerator RandomizeVelocity()
        {
            while (_isMoving)
            {
                yield return new WaitForSeconds(Random.Range(1.5f, 2.5f));
                SetRandomSpeed();
            }
        }

        private void Finish()
        {
            _isFinished = true;
            OnFinish?.Invoke();
            _accelerator.StartDecelerate(_carView, () => _isMoving = false);
        }

        private void SetRandomSpeed() =>
            _currentSpeed = Random.Range(_minSpeed, _maxSpeed);

        private void CalculateCurrentVelocity() =>
            _currentVelocity.Set(_accelerator.GetAcceleration() * _currentSpeed, 0f, 0f);

        ~CarMovement() => _carView.OnViewUpdate -= Move;
    }
}