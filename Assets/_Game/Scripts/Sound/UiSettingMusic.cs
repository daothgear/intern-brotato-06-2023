using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSettingMusic : MonoBehaviour {
  public Slider musicSlider;
  public Slider sfxSlider;

  private void Start() {
    LoadVolumeSettings();
  }

  public void MusicVolume() {
    AudioManager.Ins.MusicVolume(musicSlider.value);
    SaveVolumeSettings(Constants.PrefsKey_MusicVolume, musicSlider.value);
  }

  public void SFXVolume() {
    AudioManager.Ins.SfxVolume(sfxSlider.value);
    SaveVolumeSettings(Constants.PrefsKey_SfxVolume, sfxSlider.value);
  }

  private void SaveVolumeSettings(string prefsKey, float value) {
    PlayerPrefs.SetFloat(prefsKey, value);
    PlayerPrefs.Save();
  }

  private void LoadVolumeSettings() {
    if (PlayerPrefs.HasKey(Constants.PrefsKey_MusicVolume)) {
      float musicVolume = PlayerPrefs.GetFloat(Constants.PrefsKey_MusicVolume);
      musicSlider.value = musicVolume;
      AudioManager.Ins.MusicVolume(musicVolume);
    }

    if (PlayerPrefs.HasKey(Constants.PrefsKey_SfxVolume)) {
      float sfxVolume = PlayerPrefs.GetFloat(Constants.PrefsKey_SfxVolume);
      sfxSlider.value = sfxVolume;
      AudioManager.Ins.SfxVolume(sfxVolume);
    }
  }
}
