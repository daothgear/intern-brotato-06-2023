using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterInfoCanvas : MonoBehaviour
{
    public void ReturnToPreviousScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        int previousSceneIndex = SceneManager.GetSceneByName(currentSceneName).buildIndex - 1;
        SceneManager.LoadScene(previousSceneIndex);
    }
}
