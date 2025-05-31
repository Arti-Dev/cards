using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused = false;

    void Start()
    {
        if (pauseMenuUI == null)
        {
            Debug.LogError("PauseMenu: Pause Menu UI reference is not set in the inspector!");
        }
        else
        {
            pauseMenuUI.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(isPaused);
        }
        
        Time.timeScale = isPaused ? 0f : 1f;
        Debug.Log($"PauseMenu: Game {(isPaused ? "paused" : "unpaused")}");
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            TogglePause();
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();

    }

    void OnDestroy()
    {
        // Ensure time scale is reset when the script is destroyed
        Time.timeScale = 1f;
    }
} 