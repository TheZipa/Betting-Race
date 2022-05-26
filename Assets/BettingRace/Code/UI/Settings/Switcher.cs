using System;
using BettingRace.Code.Data.Sound;
using BettingRace.Code.Infrastructure.DI;
using BettingRace.Code.Services.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace BettingRace.Code.UI.Settings
{
    public class Switcher : MonoBehaviour
    {
        public event Action<bool> OnSwitched;
        
        [SerializeField] private GameObject _enabledView;
        [SerializeField] private GameObject _disabledView;
        [SerializeField] private Button _switchButton;

        private SoundService _soundService;
        private bool _enabled = true;

        private void Awake()
        {
            _soundService = AllServices.Container.Single<SoundService>();
            
            _switchButton.onClick.AddListener(Switch);
            _switchButton.onClick.AddListener(() => _soundService.PlaySound(SoundType.Click));
        }

        private void OnDestroy() =>
            _switchButton.onClick.RemoveAllListeners();

        public void Switch()
        {
            _enabled = !_enabled;
            
            if(_enabled) SetEnabledView();
            else SetDisabledView();
            
            OnSwitched?.Invoke(_enabled);
        }

        private void SetEnabledView()
        {
            _enabledView.SetActive(true);
            _disabledView.SetActive(false);
        }

        private void SetDisabledView()
        {
            _enabledView.SetActive(false);
            _disabledView.SetActive(true);
        }
    }
}