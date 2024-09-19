using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

namespace Managers {
public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject uiManager;
    [SerializeField] private GameObject soundManager;
    
    public bool isPaused { get; set; }
    
    private int clueCounter;

    public static GameManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }
    
    public void Update() {
        if (!isPaused && Input.GetKeyDown(KeyCode.Escape)) {
            Pausing(true);
        }
        else if (isPaused && Input.GetKeyDown(KeyCode.Escape)) {
            Pausing(false);
        }
    }

    public void IncrementClueCounter() {
        clueCounter++;

        if (clueCounter >= 3) {
            LoadScene("TestEndScene", false);
        }
    }

    public void CaptureMouse(bool isCaptured) {
        Cursor.lockState = isCaptured ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void LoadScene(string sceneName, bool captureMouse) {
        CaptureMouse(captureMouse);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
    
    
    public void Pausing(bool pausing) {
        isPaused = pausing;
        UIManager.Instance.PauseGame(isPaused);
    }




}
    
}

