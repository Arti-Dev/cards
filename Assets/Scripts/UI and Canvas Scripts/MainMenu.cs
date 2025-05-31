using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameplaySceneName;
    [SerializeField] private string creditsSceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (string.IsNullOrEmpty(gameplaySceneName))
        {
            Debug.LogError("MainMenu: Gameplay scene name is not set in the inspector!");
        }
        if (string.IsNullOrEmpty(creditsSceneName))
        {
            Debug.LogError("MainMenu: Credits scene name is not set in the inspector!");
        }
    }

    public void PlayGame()
    {
        
        try
        {
            SceneManager.LoadScene(gameplaySceneName);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"MainMenu: Failed to load gameplay scene '{gameplaySceneName}'. Error: {e.Message}");
        }
    }

    public void GoToCredits()
    {
        try
        {
            SceneManager.LoadScene(creditsSceneName);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"MainMenu: Failed to load credits scene '{creditsSceneName}'. Error: {e.Message}");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();

    }
}
