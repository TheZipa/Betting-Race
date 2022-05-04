using System;
using UnityEngine;
using UnityEngine.UI;

namespace BettingRace.Code.UI
{
    public class StartRacePanel : MonoBehaviour
    {
        public event Action OnStartRace;
        
        [SerializeField] private Image _selectedCar;
        [SerializeField] private Button _startRaceButton;

        private void Awake() =>
            _startRaceButton.onClick.AddListener(SendStartRaceAction);

        private void OnDestroy() =>
            _startRaceButton.onClick.RemoveListener(SendStartRaceAction);

        private void SendStartRaceAction() =>
            OnStartRace?.Invoke();

        public void SetSelectedCarSprite(Sprite selectedCar) =>
            _selectedCar.sprite = selectedCar;
    }
}