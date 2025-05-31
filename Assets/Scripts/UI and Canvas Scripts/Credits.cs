using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName;

    public void ReturnToMainMenu()
    {
        if (string.IsNullOrEmpty(mainMenuSceneName))
        {
            Debug.LogError("Main menu scene name is not set in the Credits script! Please set the scene name in the inspector.");
            return;
        }

        try
        {
            SceneManager.LoadScene(mainMenuSceneName);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load main menu scene: {e.Message}\nPlease check if the scene name '{mainMenuSceneName}' exists in your build settings.");
        }
    }

}
