using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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

        public bool isPaused { get; set; }
        

   

    public void pausing(bool pausing)
        {
                isPaused = pausing;
                UIManager.Instance.pauseGame(isPaused);
        }

    public void returntoMain()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void Update()
        {
            

            if (!isPaused && Input.GetKeyDown(KeyCode.Escape))
            {
                pausing(true);

            }
            else if (isPaused && Input.GetKeyDown(KeyCode.Escape))
            {
                pausing(false);
                
            }
        }


    }
}
