using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneType {
    MainMenu = 1,
    GamePlay = 2 
}
public class SceneLoaderButton : MonoBehaviour
{
    [SerializeField] private SceneType _sceneType;
    private SceneLoadController _sceneLoadController;
    private void Awake() =>
        GetComponent<Button>().onClick.AddListener(() => StartLoadScene((int)_sceneType));

    private void Start() {
        var services = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var service in services)
            if (service.TryGetComponent<SceneLoadController>(out var controller))
                _sceneLoadController = controller;
    }
    
    private void StartLoadScene(int id) =>
        _sceneLoadController.LoadSceneAsync(id);
}
