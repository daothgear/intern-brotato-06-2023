using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBackSetting : MonoBehaviour
{
    public void GoBack()
    {
        int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        SceneManager.LoadScene(previousSceneIndex);
    }
}
