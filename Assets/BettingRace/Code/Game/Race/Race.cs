using System;
using System.Collections.Generic;
using BettingRace.Code.Game.Car;
using BettingRace.Code.UI;
using Cinemachine;
using UnityEngine;

namespace BettingRace.Code.Game.Race
{
    public class Race : IRace
    {
        public event Action<int, int> OnCarFinished;
        public event Action<bool> OnRaceEnded;
        
        private readonly List<ICar> _cars;
        private readonly List<ICar> _finishedCars = new List<ICar>(4);
        private readonly CinemachineVirtualCamera _virtualCamera;
        private readonly RaceProgressSliderGroup _carProgressSliders;

        private ICar _chosenCar;

        public Race(List<ICar> cars, CinemachineVirtualCamera virtualCamera, RaceProgressSliderGroup carProgressSliders)
        {
            _cars = cars;
            _virtualCamera = virtualCamera;
            _carProgressSliders = carProgressSliders;

            SetChosenCar(0);
            SubscribeCars();
        }

        public void StartRace()
        {
            SetCameraFollow(_chosenCar.GetTransform());
            _carProgressSliders.Show();

            foreach (ICar car in _cars)
                car.Start();
        }

        public void SetChosenCar(int carId) =>
            _chosenCar = _cars[carId];

        private void OnCarFinish(ICar finishedCar)
        {
            if(finishedCar.Id == _chosenCar.Id) SetCameraFollow(null);
            _finishedCars.Add(finishedCar);
            OnCarFinished?.Invoke(_finishedCars.Count, finishedCar.Id);

            if (IsRaceEnded())
            {
                _carProgressSliders.Hide();
                OnRaceEnded?.Invoke(IsWin());
            }
        }
        
        private void SubscribeCars()
        {
            foreach (ICar car in _cars)
            {
                car.OnMoved += _carProgressSliders.RefreshSliderValue;
                car.OnFinish += OnCarFinish;
            }
        }

        private void UnsubscribeCars()
        {
            foreach (ICar car in _cars)
            {
                car.OnMoved += _carProgressSliders.RefreshSliderValue;
                car.OnFinish -= OnCarFinish;
            }
        }
        
        private void SetCameraFollow(Transform car) =>
            _virtualCamera.m_Follow = car;

        private bool IsRaceEnded() => _finishedCars.Count == _cars.Count;

        private bool IsWin() => _finishedCars[0].Id == _chosenCar.Id;

        ~Race() => UnsubscribeCars();
    }
}