using BettingRace.Code.Data;
using BettingRace.Code.Data.Sound;
using BettingRace.Code.Data.StaticData;
using BettingRace.Code.Services.SaveLoad;
using BettingRace.Code.Services.Sound;
using BettingRace.Code.UI.FinishRace;
using UnityEngine;

namespace BettingRace.Code.Game.BetResult
{
    public class BetResultCalculator : IBetResultCalculator
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly SoundService _soundService;
        private readonly FinishRacePanel _finishPanel;
        private readonly Color _winTextColor;
        private readonly Color _loseTextColor;
        private readonly int _betFactor;

        private const char Plus = '+';
        private const char Minus = '-';

        private int _playerBalance;
        private int _bet;

        public BetResultCalculator(ISaveLoadService saveLoadService, SoundService soundService, FinishRacePanel finishPanel, BetStaticData betData)
        {
            _saveLoadService = saveLoadService;
            _finishPanel = finishPanel;
            _soundService = soundService;

            _winTextColor = betData.WinTextColor;
            _loseTextColor = betData.LoseTextColor;
            _betFactor = betData.BetFactor;
        }

        public void SetBet(int bet) => _bet = bet;

        public void CalculateBetResult(bool isWin)
        {
            SoundType resultSound;
            if (isWin)
            {
                MultiplyPlayerBalance();
                SetWinBetText();
                resultSound = SoundType.Win;
            }
            else
            {
                MinusPlayerBalance();
                SetLoseBetText();
                resultSound = SoundType.Lose;
            }

            _soundService.PlaySound(resultSound);
            _saveLoadService.SaveProgress(this);
            _finishPanel.Show();
        }

        public void LoadProgress(PlayerProgress progress) =>
            _playerBalance = progress.Balance;

        public void UpdateProgress(PlayerProgress progress) =>
            progress.Balance = _playerBalance;

        private void SetWinBetText()
        {
            string winBet = Plus + _bet.ToCultureString();
            _finishPanel.SetBetResultText(winBet, _winTextColor);
        }

        private void SetLoseBetText()
        {
            string loseBet = Minus + _bet.ToCultureString();
            _finishPanel.SetBetResultText(loseBet, _loseTextColor);
        }

        private void MultiplyPlayerBalance()
        {
            _bet *= _betFactor;
            _playerBalance += _bet;
        }

        private void MinusPlayerBalance() => _playerBalance -= _bet;
    }
}