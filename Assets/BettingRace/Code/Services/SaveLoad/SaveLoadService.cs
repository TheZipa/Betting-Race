using BettingRace.Code.Data;
using BettingRace.Code.Services.Factories;
using BettingRace.Code.Services.PersistentProgress;
using UnityEngine;

namespace BettingRace.Code.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IPersistentProgressService _progressService;
        private readonly IUIFactory _uiFactory;
        private const string ProgressKey = "Progress";

        public SaveLoadService(IPersistentProgressService progressService, IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
            _progressService = progressService;
        }
        
        public void SaveProgress(ISavedProgress progressWriter)
        {
            progressWriter.UpdateProgress(_progressService.Progress);
            
            Debug.Log("Progress has been saved. Balance = " + _progressService.Progress.Balance);
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
    }
}