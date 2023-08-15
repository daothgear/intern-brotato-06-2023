using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterInfoCanvas : MonoBehaviour {
  public void ReturnToPreviousScene() {
    AudioManager.Ins.PlaySfx(SoundName.SfxClickButton);
    string currentSceneName = SceneManager.GetActiveScene().name;
    int previousSceneIndex = SceneManager.GetSceneByName(currentSceneName).buildIndex - 1;
    SceneManager.LoadScene(previousSceneIndex);
  }

  public void NextToPreviousScene() {
    AudioManager.Ins.PlaySfx(SoundName.SfxClickButton);
    string currentSceneName = SceneManager.GetActiveScene().name;
    int nextSceneIndex = SceneManager.GetSceneByName(currentSceneName).buildIndex + 1;
    SceneManager.LoadScene(nextSceneIndex);
  }
}