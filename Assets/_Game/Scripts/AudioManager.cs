using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> {
  public Sound[] musicSounds, sfxSounds;
  public AudioSource musicSource, sfxSource;

  private void Awake() {
    DontDestroyOnLoad(gameObject);
  }

  private void Start() {
    if (PlayerPrefs.HasKey(Constants.PrefsKey_MusicVolume)) {
      float musicVolume = PlayerPrefs.GetFloat(Constants.PrefsKey_MusicVolume);
      musicSource.volume = musicVolume;
    }

    if (PlayerPrefs.HasKey(Constants.PrefsKey_SfxVolume)) {
      float sfxVolume = PlayerPrefs.GetFloat(Constants.PrefsKey_SfxVolume);
      sfxSource.volume = sfxVolume;
    }

    PlayMusic(SoundName.BackGroundMusic);
  }

  public void PlayMusic(SoundName soundName) {
    Sound s = Array.Find(musicSounds, x => x.name == soundName);

    if (s == null) {
      Debug.Log("Sound Not Found");
    }
    else {
      musicSource.clip = s.clip;
      musicSource.Play();
    }
  }
  
  public void PlaySfx(SoundName soundName) {
    Sound s = Array.Find(sfxSounds, x => x.name == soundName);

    if (s == null) {
      Debug.Log("Sound Not Found");
    }
    else {
      sfxSource.clip = s.clip;
      sfxSource.Play();
    }
  }

  public void ToggleMusic() {
    musicSource.mute = !musicSource.mute;
  }
  
  public void ToggleSFX() {
    sfxSource.mute = !sfxSource.mute;
  }

  public void MusicVolume(float volume) {
    musicSource.volume = volume;
  }

  public void SfxVolume(float volume) {
    sfxSource.volume = volume;
  }
}