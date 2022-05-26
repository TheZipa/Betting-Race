using BettingRace.Code.Data.Sound;
using BettingRace.Code.Infrastructure.DI;
using BettingRace.Code.Services.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace BettingRace.Code.UI.Settings
{
    public class SettingsPanel : MonoBehaviour
    {
        public Switcher SoundSwitcher;
        
        [SerializeField] private GameObject _settingsView;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _backgroundCloseButton;
        [SerializeField] private Button _quitButton;

        private SoundService _soundService;

        private void Awake()
        {
            _soundService = AllServices.Container.Single<SoundService>();
            _settingsButton.onClick.AddListener(() => _settingsView.SetActive(!_settingsView.activeSelf));
            _backgroundCloseButton.onClick.AddListener(() => _settingsView.SetActive(false));
            _quitButton.onClick.AddListener(Application.Quit);

            _settingsButton.onClick.AddListener(() => _soundService.PlaySound(SoundType.Click));
            _backgroundCloseButton.onClick.AddListener(() => _soundService.PlaySound(SoundType.Click));
            _quitButton.onClick.AddListener(() => _soundService.PlaySound(SoundType.Click));
        }

        private void OnDestroy()
        {
            _settingsButton.onClick.RemoveAllListeners();
            _backgroundCloseButton.onClick.RemoveAllListeners();
            _quitButton.onClick.RemoveAllListeners();
        }
    }
}