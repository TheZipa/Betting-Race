using System;
using UnityEngine;
using UnityEngine.UI;

namespace BettingRace.Code.UI.StartRace
{
    public class StartRacePanel : MonoBehaviour
    {
        public event Action OnStartRace;
        
        [SerializeField] private Image _selectedHorseImage;
        [SerializeField] private Button _startRaceButton;

        private void Awake() =>
            _startRaceButton.onClick.AddListener(SendStartRaceAction);

        private void OnDestroy() =>
            _startRaceButton.onClick.RemoveListener(SendStartRaceAction);

        private void SendStartRaceAction() =>
            OnStartRace?.Invoke();

        public void SetSelectedHorseSprite(Sprite selectedHorse) =>
            _selectedHorseImage.sprite = selectedHorse;
    }
}