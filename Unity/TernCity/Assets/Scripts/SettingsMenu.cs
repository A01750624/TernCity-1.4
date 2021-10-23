using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
    public AudioMixer mixer;
    public Slider musicSlider, sfxSlider;

    private void Awake() {
        musicSlider.value = PlayerPrefs.GetFloat("PrefMusicVolume", 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat("PrefSfxVolume", 0.75f);
    }

    public void SetFullScreen(bool isFullScreen) {
        Screen.fullScreen = isFullScreen;
    }

    public void SetMusicVolume(float sliderValue) {
        mixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("PrefMusicVolume", sliderValue);
    }

    public void SetSfxVolume(float sliderValue) {
        mixer.SetFloat("SfxVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("PrefSfxVolume", sliderValue);
    }

    void PauseGame() {
        Time.timeScale = 0;
    }

    void ResumeGame() {
        Time.timeScale = 1;
    }

}
