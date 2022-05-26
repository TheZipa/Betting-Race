using System;
using System.Collections.Generic;
using BettingRace.Code.Data;
using BettingRace.Code.Services.SaveLoad;

namespace BettingRace.Code.UI.Bet
{
    public class BetModifier : IBetModifier
    {
        public event Action<string, string> OnBetRefresh;
        public event Action<int> OnBetApprove;
        
        private readonly Stack<int> _betHistory = new Stack<int>();
        private readonly ISaveLoadService _saveLoadService;
        private readonly int _minBet;

        private int _currentBet;
        private int _playerBalance;

        public BetModifier(ISaveLoadService saveLoadService, int minBet)
        {
            _saveLoadService = saveLoadService;
            _currentBet = _minBet = minBet;
        }

        public void AddBet(int bet)
        {
            if (IsMoreThanBalance(_currentBet + bet)) return;

            _betHistory.Push(bet);
            _currentBet += bet;
            RefreshBetView();
        }

        public void UndoBet()
        {
            if (_betHistory.Count == 0) return;
            MinusBet(_betHistory.Pop());
        }

        public void ClearBet()
        {
            _currentBet = _minBet;
            RefreshBetView();
        }

        public void ApproveBet()
        {
            _playerBalance -= _currentBet;
            OnBetApprove?.Invoke(_currentBet);
            _currentBet = 0;
            _saveLoadService.SaveProgress(this);
            RefreshBetView();
        }

        public void RefreshBetView()
        {
            int balance = _playerBalance - _currentBet;
            string balanceText = balance > 0 ? balance.ToCultureString() : "0";
            OnBetRefresh?.Invoke(balanceText, _currentBet.ToCultureString());
        }

        public void LoadProgress(PlayerProgress progress) =>
            _playerBalance = progress.Balance;

        public void UpdateProgress(PlayerProgress progress) =>
            progress.Balance = _playerBalance;

        private void MinusBet(int bet)
        {
            _currentBet -= bet;
            RefreshBetView();
        }

        private bool IsMoreThanBalance(int newBalance) =>
            newBalance > _playerBalance;
    }
}