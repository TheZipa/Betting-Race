using BettingRace.Code.Data;
using BettingRace.Code.Infrastructure.DI;
using BettingRace.Code.Services.PersistentProgress;

namespace BettingRace.Code.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress(ISavedProgress progressWriter);
        PlayerProgress LoadProgress();
    }
}