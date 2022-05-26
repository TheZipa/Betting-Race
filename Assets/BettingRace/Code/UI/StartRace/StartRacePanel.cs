using System;
using BettingRace.Code.Data.Sound;
using BettingRace.Code.Infrastructure.DI;
using BettingRace.Code.Services.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace BettingRace.Code.UI.StartRace
{
    public class StartRacePanel : MonoBehaviour
    {
        public event Action OnStartRace;
        
        [SerializeField] private Image _selectedHorseImage;
        [SerializeField] private Button _startRaceButton;

        private SoundService _soundService;

        private void Awake()
        {
            _soundService = AllServices.Container.Single<SoundService>();
            _startRaceButton.onClick.AddListener(SendStartRaceAction);
            _startRaceButton.onClick.AddListener(() => _soundService.PlaySound(SoundType.Click));
        }

        private void OnDestroy() =>
            _startRaceButton.onClick.RemoveAllListeners();

        private void SendStartRaceAction() =>
            OnStartRace?.Invoke();

        public void SetSelectedHorseSprite(Sprite selectedHorse) =>
            _selectedHorseImage.sprite = selectedHorse;
    }
}