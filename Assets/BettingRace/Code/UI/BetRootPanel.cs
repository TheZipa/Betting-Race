using BettingRace.Code.UI.Bet;
using BettingRace.Code.UI.StartRace;
using UnityEngine;

namespace BettingRace.Code.UI
{
    public class BetRootPanel : MonoBehaviour
    {
        public ResetBalanceLabel ResetBalanceLabel;
        [SerializeField] private StartRacePanel _startRacePanel;

        private void Awake() =>
            _startRacePanel.OnStartRace += Disable;

        private void OnDestroy() =>
            _startRacePanel.OnStartRace -= Disable;

        private void Disable() => gameObject.SetActive(false);
    }
}