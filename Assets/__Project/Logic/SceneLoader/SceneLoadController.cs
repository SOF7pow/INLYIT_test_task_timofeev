using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneLoadController : MonoBehaviour {
    
    public Action<float> OnLoadProgressChange;
    public Action OnLoadStart;
    public Action OnLoadFinish;
    public void Start() =>
        StartCoroutine(MockLoadProgress(1));

    public void LoadSceneAsync(int id) => StartCoroutine(MockLoadProgress(id));

    private async void LoadScene(int id) {
        if (SceneManager.sceneCount == 1) {
            
        }
        var operation = SceneManager.LoadSceneAsync(id, LoadSceneMode.Additive);
        operation.allowSceneActivation = false;
        while (!operation.isDone) {
            if (operation.progress >= 0.9f) operation.allowSceneActivation = true;
            await Task.Yield();
        }
    }

    private IEnumerator MockLoadProgress(int id) {
        OnLoadStart?.Invoke();
        
        if (SceneManager.loadedSceneCount > 1) UnloadSceneAsync(id == 1 ? 2 : 1);
        
        var elapsedTime = 0f;
        while (elapsedTime < 2f) {
            var value = Mathf.Lerp(0f, 1f, elapsedTime / 2f);
            elapsedTime += Time.deltaTime;
            OnLoadProgressChange?.Invoke(value);
            yield return null;
        }
        
        OnLoadFinish?.Invoke();
        LoadScene(id);
    }
    private async void UnloadSceneAsync(int id) {
        var operation = SceneManager.UnloadSceneAsync(id);
        while (!operation.isDone) 
            await Task.Yield();
    }
}
