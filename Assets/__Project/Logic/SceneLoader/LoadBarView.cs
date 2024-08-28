using UnityEngine;
using UnityEngine.UI;

namespace _multiSceneTest {
    public class LoadBarView : MonoBehaviour {

        [SerializeField] private Slider _loadingBar;
        private PanelComponent _panelComponent;
        private SceneLoadController _sceneLoader;
        private void Awake() {
            _sceneLoader = FindObjectOfType<SceneLoadController>();
            _panelComponent = GetComponentInChildren<PanelComponent>();
            _panelComponent.gameObject.SetActive(false);
        }
        private void OnEnable() {
            _sceneLoader.OnLoadProgressChange += UpdateView;
            _sceneLoader.OnLoadStart += SwitchActiveState;
            _sceneLoader.OnLoadFinish += SwitchActiveState;
        }
        private void OnDisable() {
            _sceneLoader.OnLoadProgressChange -= UpdateView;
            _sceneLoader.OnLoadStart -= SwitchActiveState;
            _sceneLoader.OnLoadFinish -= SwitchActiveState;
        }

        private void UpdateView(float value) => _loadingBar.value = value;
        private void SwitchActiveState() => _panelComponent.gameObject.SetActive(!_panelComponent.gameObject.activeSelf);
    }
}
