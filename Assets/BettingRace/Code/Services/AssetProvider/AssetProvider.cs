using UnityEngine;

namespace BettingRace.Code.Services.AssetProvider
{
    public class AssetProvider : IAssetProvider
    {
        private const string HorsePrefab = "Prefabs/Horse";
        private const string CameraPrefab = "Prefabs/Camera/CM Virtual Camera";
        private const string BackgroundParallaxPrefab = "Prefabs/Environment/BackgroundParallax";
        private const string TribunesPrefab = "Prefabs/Environment/Tribunes";
        private const string StartFinishLinePrefab = "Prefabs/Environment/StartFinishLine";

        public GameObject GetHorsePrefab() =>
            Resources.Load<GameObject>(HorsePrefab);

        public GameObject GetCameraPrefab() =>
            Resources.Load<GameObject>(CameraPrefab);

        public GameObject GetBackgroundParallaxPrefab() =>
            Resources.Load<GameObject>(BackgroundParallaxPrefab);

        public GameObject GetTribunesPrefab() =>
            Resources.Load<GameObject>(TribunesPrefab);

        public GameObject GetStartFinishLinePrefab() =>
            Resources.Load<GameObject>(StartFinishLinePrefab);
    }
}