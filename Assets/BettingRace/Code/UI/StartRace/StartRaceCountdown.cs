using System;
using System.Collections;
using BettingRace.Code.Data.Sound;
using BettingRace.Code.Infrastructure.DI;
using BettingRace.Code.Services.Sound;
using UnityEngine;

namespace BettingRace.Code.UI.StartRace
{
    public class StartRaceCountdown : MonoBehaviour
    {
        public event Action OnCountdownEnded;
        
        [SerializeField] private GameObject[] _countdownNumbers;
        [SerializeField] private GameObject _startText;

        private SoundService _soundService;

        private void Awake() =>
            _soundService = AllServices.Container.Single<SoundService>();

        public void StartCountdown() =>
            StartCoroutine(Countdown());

        private IEnumerator Countdown()
        {
            _soundService.MuteBackgroundMusic();
            SoundType sound = SoundType.Three;
            
            foreach (GameObject number in _countdownNumbers)
            {
                number.SetActive(true);
                _soundService.PlaySound(sound++);
                yield return new WaitForSeconds(1);
                number.SetActive(false);
            }
            
            _soundService.PlaySound(SoundType.Go);
            _startText.SetActive(true);
            OnCountdownEnded?.Invoke();
        }
    }
}