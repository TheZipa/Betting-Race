using System;
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

        private void Awake() =>
            _newRaceButton.onClick.AddListener(() => OnNewRace?.Invoke());

        private void OnDestroy() =>
            _newRaceButton.onClick.RemoveListener(() => OnNewRace?.Invoke());

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