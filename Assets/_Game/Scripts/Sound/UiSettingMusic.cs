using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSettingMusic : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    
    public void MusicVolumn() {
        AudioManager.Ins.MusicVolume(musicSlider.value);
    }

    public void SFXVolumn() {
        AudioManager.Ins.SfxVolume(sfxSlider.value);
    }
}
