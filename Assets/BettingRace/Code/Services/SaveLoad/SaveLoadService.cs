using BettingRace.Code.Data;
using BettingRace.Code.Services.PersistentProgress;
using UnityEngine;

namespace BettingRace.Code.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IPersistentProgressService _progressService;
        private const string ProgressKey = "Progress";

        public SaveLoadService(IPersistentProgressService progressService) =>
            _progressService = progressService;

        public void SaveProgress(ISavedProgress progressWriter)
        {
            progressWriter.UpdateProgress(_progressService.Progress);
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
    }
}