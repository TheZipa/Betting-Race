using System;
using System.Collections;
using UnityEngine;

namespace BettingRace.Code.UI.StartRace
{
    public class StartRaceCountdown : MonoBehaviour
    {
        public event Action OnCountdownEnded;
        
        [SerializeField] private GameObject[] _countdownNumbers;
        [SerializeField] private GameObject _startText;

        public void StartCountdown() =>
            StartCoroutine(Countdown());

        private IEnumerator Countdown()
        {
            foreach (GameObject number in _countdownNumbers)
            {
                number.SetActive(true);
                yield return new WaitForSeconds(1);
                number.SetActive(false);
            }
            
            _startText.SetActive(true);
            OnCountdownEnded?.Invoke();
        }
    }
}