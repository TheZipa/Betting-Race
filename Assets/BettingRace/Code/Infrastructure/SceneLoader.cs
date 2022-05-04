using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BettingRace.Code.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
            => _coroutineRunner = coroutineRunner;

        public void Load(string name, Action onLoaded = null)
            => _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

        private IEnumerator LoadScene(string scene, Action onLoaded = null)
        {
            // if (SceneManager.GetActiveScene().name == scene)
            // {
            //     onLoaded?.Invoke();
            //     yield break;
            // }

            AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(scene);

            while (!sceneLoading.isDone)
                yield return null;
            
            onLoaded?.Invoke();
        }
    }
}