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
    SaveVolumeSettings();
  }

  public void SFXVolume() {
    AudioManager.Ins.SfxVolume(sfxSlider.value);
    SaveVolumeSettings();
  }

  private void SaveVolumeSettings() {
    PlayerPrefs.SetFloat(Constants.PrefsKey_SettingVolume, musicSlider.value);
    PlayerPrefs.SetFloat(Constants.PrefsKey_SettingVolume, sfxSlider.value);
    PlayerPrefs.Save();
  }

  private void LoadVolumeSettings() {
    if (PlayerPrefs.HasKey(Constants.PrefsKey_SettingVolume)) {
      float musicVolume = PlayerPrefs.GetFloat(Constants.PrefsKey_SettingVolume);
      float sfxVolume = PlayerPrefs.GetFloat(Constants.PrefsKey_SettingVolume);
      musicSlider.value = musicVolume;
      sfxSlider.value = sfxVolume;
      AudioManager.Ins.MusicVolume(musicVolume);
      AudioManager.Ins.SfxVolume(sfxVolume);
    }
  }
}