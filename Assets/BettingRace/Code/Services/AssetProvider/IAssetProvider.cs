using BettingRace.Code.Infrastructure.DI;
using UnityEngine;

namespace BettingRace.Code.Services.AssetProvider
{
    public interface IAssetProvider : IService
    {
        GameObject GetHorsePrefab();
        GameObject GetCameraPrefab();
        GameObject GetBackgroundParallaxPrefab();
        GameObject GetTribunesPrefab();
        GameObject GetStartFinishLinePrefab();
    }
}