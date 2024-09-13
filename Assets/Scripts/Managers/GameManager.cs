using UnityEngine;

namespace Managers {
public class GameManager : MonoBehaviour {
    [SerializeField] private GameObject uiManager;
    [SerializeField] private GameObject soundManager;

    public static GameManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }
}
}
