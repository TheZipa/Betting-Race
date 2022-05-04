using System;
using BettingRace.Code.Data;
using BettingRace.Code.Data.StaticData;
using BettingRace.Code.Services.SaveLoad;
using BettingRace.Code.UI.FinishRace;
using UnityEngine;

namespace BettingRace.Code.Game.BetResult
{
    public class BetResultCalculator : IBetResultCalculator
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly FinishRacePanel _finishPanel;
        private readonly Color _winTextColor;
        private readonly Color _loseTextColor;
        private readonly int _betFactor;

        private const char Plus = '+';
        private const char Minus = '-';

        private int _playerBalance;
        private int _bet;

        public BetResultCalculator(ISaveLoadService saveLoadService, FinishRacePanel finishPanel, BetStaticData betData)
        {
            _saveLoadService = saveLoadService;
            _finishPanel = finishPanel;

            _winTextColor = betData.WinTextColor;
            _loseTextColor = betData.LoseTextColor;
            _betFactor = betData.BetFactor;
        }

        public void SetBet(int bet) => _bet = bet;

        public void CalculateBetResult(bool isWin)
        {
            UpdatePlayerBalance(isWin);
            string resultBet = GetBetResultString(isWin);
            Color resultColor = isWin ? _winTextColor : _loseTextColor;
            
            _finishPanel.SetBetResultText(resultBet, resultColor);
            _finishPanel.Show();
        }

        private string GetBetResultString(bool isWin)
        {
            string resultBet = String.Empty;
            resultBet += isWin ? Plus : Minus;
            resultBet += _bet.ToCultureString();
            return resultBet;
        }

        private void UpdatePlayerBalance(bool isWin)
        {
            if (isWin)
            {
                _bet *= _betFactor;
                _playerBalance += _bet;
            }
            else
            {
                _playerBalance -= _bet;
            }
            
            _saveLoadService.SaveProgress(this);
        }

        public void LoadProgress(PlayerProgress progress) =>
            _playerBalance = progress.Balance;

        public void UpdateProgress(PlayerProgress progress) =>
            progress.Balance = _playerBalance;
    }
}