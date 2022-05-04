using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BettingRace.Code.UI.Bet
{
    public class BetPanel : MonoBehaviour
    {
        public event Action<int> OnAddBet;

        public event Action OnClearBet;
        public event Action OnUndoBet;
        
        [SerializeField] private TextMeshProUGUI _balanceText;
        [SerializeField] private TextMeshProUGUI _betText;
        [SerializeField] private TextMeshProUGUI[] _addBetButtonTexts;
        [SerializeField] private Button[] _addBetButtons;
        [SerializeField] private Button _clearButton;
        [SerializeField] private Button _undoButton;

        private void Awake()
        {
            _clearButton.onClick.AddListener(() => OnClearBet?.Invoke());
            _undoButton.onClick.AddListener(() => OnUndoBet?.Invoke());
        }
        
        private void OnDestroy()
        {
            _clearButton.onClick.RemoveAllListeners();
            _undoButton.onClick.RemoveAllListeners();

            foreach (Button button in _addBetButtons)
                button.onClick.RemoveAllListeners();
        }

        public void Construct(int[] betValues)
        {
            for(int i = 0; i < _addBetButtons.Length; i++)
                InitializeBetButton(i, betValues[i]);
        }

        public void RefreshTexts(string balance, string bet)
        {
            _balanceText.text = balance;
            _betText.text = bet;
        }

        private void InitializeBetButton(int buttonIndex, int betValue)
        {
            _addBetButtons[buttonIndex].onClick.AddListener(() => OnAddBet?.Invoke(betValue));
            _addBetButtonTexts[buttonIndex].text = betValue.ToString();
        }
    }
}