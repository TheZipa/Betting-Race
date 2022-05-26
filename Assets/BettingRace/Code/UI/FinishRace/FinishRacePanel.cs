using System;
using BettingRace.Code.Data.Sound;
using BettingRace.Code.Infrastructure.DI;
using BettingRace.Code.Services.Sound;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BettingRace.Code.UI.FinishRace
{
    public class FinishRacePanel : MonoBehaviour
    {
        public event Action OnNewRace;
        public Transform FinishedHorseContent;
        
        [SerializeField] private TextMeshProUGUI _betResultText;
        [SerializeField] private Button _newRaceButton;

        private SoundService _soundService;

        private void Awake()
        {
            _soundService = AllServices.Container.Single<SoundService>();
            _newRaceButton.onClick.AddListener(() => OnNewRace?.Invoke());
            _newRaceButton.onClick.AddListener(() => _soundService.PlaySound(SoundType.Click));
        }

        private void OnDestroy() =>
            _newRaceButton.onClick.RemoveAllListeners();

        public void SetBetResultText(string result, Color resultColor)
        {
            _betResultText.text = result;
            _betResultText.color = resultColor;
        }

        public void Show() =>
            gameObject.SetActive(true);

        public void Hide() =>
            gameObject.SetActive(false);
    }
}