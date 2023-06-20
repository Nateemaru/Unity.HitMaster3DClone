using System;
using System.Collections;
using _Scripts.Services.CoroutineRunnerService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Services.SceneLoadService
{
    public class SceneLoader : ISceneLoadService
    {
        private readonly ICoroutineRunner coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) => 
            this.coroutineRunner = coroutineRunner;

        public void Load(string name, Action onLoaded = null) =>
            coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
    
        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
                yield return null;
      
            onLoaded?.Invoke();
        }
    }
}