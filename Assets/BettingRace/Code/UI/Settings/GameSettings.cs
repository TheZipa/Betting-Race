using BettingRace.Code.Data;
using BettingRace.Code.Services.PersistentProgress;
using BettingRace.Code.Services.SaveLoad;
using BettingRace.Code.Services.Sound;

namespace BettingRace.Code.UI.Settings
{
    public class GameSettings : ISaveLoadProgress
    {
        private readonly Switcher _soundSwitcher;
        private readonly ISaveLoadService _saveLoadService;
        private readonly SoundService _soundService;

        private bool _isSoundsEnabled;
        
        public GameSettings(Switcher soundSwitcher, SoundService soundService, ISaveLoadService saveLoadService)
        {
            _soundService = soundService;
            _saveLoadService = saveLoadService;
            _soundSwitcher = soundSwitcher;
            
            _soundSwitcher.OnSwitched += OnSoundSwitched;
        }

        private void OnSoundSwitched(bool enabled)
        {
            _isSoundsEnabled = enabled;
            _soundService.SwitchSoundMute(enabled);
            _saveLoadService.SaveProgress(this);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _isSoundsEnabled = progress.SoundEnabled;
            
            if(_isSoundsEnabled == false) _soundSwitcher.Switch();
        }

        public void UpdateProgress(PlayerProgress progress) =>
            progress.SoundEnabled = _isSoundsEnabled;


        ~GameSettings() => _soundSwitcher.OnSwitched -= OnSoundSwitched;
    }
}