using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers {
public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject uiManager;
    [SerializeField] private GameObject soundManager;

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
}
}
