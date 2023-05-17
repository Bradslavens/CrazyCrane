using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;

    public void LoadSceneOnClick()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void EndGame()
    {
        // Add any game-ending logic here
        Application.Quit();
    }
}
