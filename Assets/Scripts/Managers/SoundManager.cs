using UnityEngine;

namespace Managers {
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

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
