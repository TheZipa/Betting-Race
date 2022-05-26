using BettingRace.Code.Data;

namespace BettingRace.Code.Services.PersistentProgress
{
    public interface ISaveLoadProgress
    {
        void LoadProgress(PlayerProgress progress);
        void UpdateProgress(PlayerProgress progress);
    }
}