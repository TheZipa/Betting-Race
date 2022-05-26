using BettingRace.Code.UI.Bet;
using BettingRace.Code.UI.FinishRace;
using BettingRace.Code.UI.Settings;
using BettingRace.Code.UI.StartRace;
using UnityEngine;

namespace BettingRace.Code.UI
{
    public class RootUI : MonoBehaviour
    {
        public Transform SelectHorseLayoutContent;
        public StartRacePanel StartRacePanel;
        public StartRaceCountdown StartRaceCountdown;
        public BetRootPanel BetRootPanel;
        public ManageBetPanel ManageBetPanel;
        public FinishRacePanel FinishRacePanel;
        public RaceProgressSliderGroup RaceProgressSliderGroup;
        public SettingsPanel SettingsPanel;
    }
}