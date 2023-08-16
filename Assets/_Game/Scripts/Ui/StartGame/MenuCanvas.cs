using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuCanvas : MonoBehaviour
{
  [SerializeField] private GameObject menuCanvas;
  [SerializeField] private GameObject guideplayCanvas;
  [SerializeField] private GameObject settingCanvas;

  [SerializeField] private MenuState currentCanvasMenu;

  private enum MenuState
  {
    MenuGame,
    GuideGame,
    SettingGame,
    StartGame,
  }

  private void Start()
  {
    currentCanvasMenu = MenuState.MenuGame;
    ShowCanvas(currentCanvasMenu);
  }


  public void StartGame()
  {
    AudioManager.Ins.PlaySfx(SoundName.SfxClickButton);
    currentCanvasMenu = MenuState.StartGame;
    SceneManager.LoadScene(Constants.Scene_GamePlay);
  }

  public void ShowSettingGameCanvas()
  {
    AudioManager.Ins.PlaySfx(SoundName.SfxClickButton);
    currentCanvasMenu = MenuState.SettingGame;
    ShowCanvas(currentCanvasMenu);
  }

  public void ShowGuildGameCanvas()
  {
    AudioManager.Ins.PlaySfx(SoundName.SfxClickButton);
    currentCanvasMenu = MenuState.GuideGame;
    ShowCanvas(currentCanvasMenu);
  }

  public void GoBack()
  {
    AudioManager.Ins.PlaySfx(SoundName.SfxClickButton);
    if (currentCanvasMenu == MenuState.SettingGame || currentCanvasMenu == MenuState.GuideGame)
    {
      currentCanvasMenu = MenuState.MenuGame;
      ShowCanvas(currentCanvasMenu);
    }
  }
  private void ShowCanvas(MenuState menuState)
  {
    menuCanvas.SetActive(menuState == MenuState.MenuGame);
    settingCanvas.SetActive(menuState == MenuState.SettingGame);
    guideplayCanvas.SetActive(menuState == MenuState.GuideGame);
  }

  public void QuitGame()
  {
    Application.Quit();
    Debug.Log("Quit");
  }
}
