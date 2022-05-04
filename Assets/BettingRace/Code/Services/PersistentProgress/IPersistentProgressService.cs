using BettingRace.Code.Data;
using BettingRace.Code.Infrastructure.DI;

namespace BettingRace.Code.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}