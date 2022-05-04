using System;
using System.Collections;
using BettingRace.Code.Infrastructure;
using UnityEngine;

namespace BettingRace.Code.Game.Car
{
    public class CarAccelerator : ICarAccelerator
    {
        private readonly AnimationCurve _accelerationCurve;
        private readonly float _accelerationStep;

        private float _acceleration;
        
        public CarAccelerator(AnimationCurve accelerationCurve, float accelerationStep)
        {
            _accelerationCurve = accelerationCurve;
            _accelerationStep = accelerationStep;
        }

        public float GetAcceleration() =>
            _accelerationCurve.Evaluate(_acceleration);

        public void StartAccelerate(ICoroutineRunner coroutineRunner, Action onFullSpeed) =>
            coroutineRunner.StartCoroutine(Accelerate(onFullSpeed));

        public void StartDecelerate(ICoroutineRunner coroutineRunner, Action onStop) =>
            coroutineRunner.StartCoroutine(Decelerate(onStop));

        private IEnumerator Accelerate(Action onFullSpeed)
        {
            while (_acceleration < 1f)
            {
                yield return new WaitForSeconds(_accelerationStep);
                _acceleration += _accelerationStep;
            }

            _acceleration = 1f;
            onFullSpeed?.Invoke();
        }

        private IEnumerator Decelerate(Action onStop)
        {
            while (_acceleration > 0)
            {
                yield return new WaitForSeconds(_accelerationStep);
                _acceleration -= _accelerationStep;
            }

            _acceleration = 0;
            onStop?.Invoke();
        }
    }
}