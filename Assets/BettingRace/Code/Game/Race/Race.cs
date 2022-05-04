using System;
using System.Collections.Generic;
using BettingRace.Code.Game.Horse;
using BettingRace.Code.UI;
using Cinemachine;
using UnityEngine;

namespace BettingRace.Code.Game.Race
{
    public class Race : IRace
    {
        public event Action<int, int> OnHorseFinished;
        public event Action<bool> OnRaceEnded;
        
        private readonly List<IHorse> _horses;
        private readonly List<IHorse> _finishedHorses = new List<IHorse>(4);
        private readonly CinemachineVirtualCamera _virtualCamera;
        private readonly RaceProgressSliderGroup _horseProgressSliders;

        private IHorse _chosenHorse;

        public Race(List<IHorse> horses, CinemachineVirtualCamera virtualCamera, RaceProgressSliderGroup horseProgressSliders)
        {
            _horses = horses;
            _virtualCamera = virtualCamera;
            _horseProgressSliders = horseProgressSliders;

            SetChosenHorse(0);
            SubscribeHorses();
        }

        public void StartRace()
        {
            SetCameraFollow(_chosenHorse.GetTransform());
            _horseProgressSliders.Show();

            foreach (IHorse horse in _horses)
                horse.Start();
        }

        public void SetChosenHorse(int horseId) =>
            _chosenHorse = _horses[horseId];

        private void OnHorseFinish(IHorse finishedHorse)
        {
            if(finishedHorse.Id == _chosenHorse.Id) SetCameraFollow(null);
            _finishedHorses.Add(finishedHorse);
            OnHorseFinished?.Invoke(_finishedHorses.Count, finishedHorse.Id);

            if (IsRaceEnded())
            {
                _horseProgressSliders.Hide();
                OnRaceEnded?.Invoke(IsWin());
            }
        }
        
        private void SubscribeHorses()
        {
            foreach (IHorse horse in _horses)
            {
                horse.OnMoved += _horseProgressSliders.RefreshSliderValue;
                horse.OnFinish += OnHorseFinish;
            }
        }

        private void UnsubscribeHorses()
        {
            foreach (IHorse horse in _horses)
            {
                horse.OnMoved += _horseProgressSliders.RefreshSliderValue;
                horse.OnFinish -= OnHorseFinish;
            }
        }
        
        private void SetCameraFollow(Transform horse) =>
            _virtualCamera.m_Follow = horse;

        private bool IsRaceEnded() => _finishedHorses.Count == _horses.Count;

        private bool IsWin() => _finishedHorses[0].Id == _chosenHorse.Id;

        ~Race() => UnsubscribeHorses();
    }
}