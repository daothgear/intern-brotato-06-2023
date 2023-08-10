using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> {
    public AudioSource source;
    private void OnDestroy() {
        if (source == null) {
            source = GetComponent<AudioSource>();
        }
    }

    public void PlaySound(AudioClip sound) {
        source.PlayOneShot(sound);
    }
}
