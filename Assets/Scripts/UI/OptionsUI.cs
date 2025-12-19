using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {


    public static OptionsUI Instance { get; private set; }


    [SerializeField] private Slider soundEffectsSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Button closeButton;

 


    private Action onCloseButtonAction;


    private void Awake() {
        Instance = this;
        soundEffectsSlider.onValueChanged.AddListener(OnSoundSliderChanged);
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        closeButton.onClick.AddListener(() => {
            Hide();
            onCloseButtonAction();
        });

      
    }
    private void OnSoundSliderChanged(float value) 
    {
        SoundManager.Instance.ChangeVolume(Mathf.RoundToInt(value));
    }
    private void OnMusicSliderChanged(float value) 
    {
        MusicManager.Instance.ChangeVolume(Mathf.RoundToInt(value));
    }
    private void Start() {
        KitchenGameManager.Instance.OnLocalGameUnpaused += KitchenGameManager_OnGameUnpaused;

        Hide();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    // private void UpdateVisual() {
    //     soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
    //     musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

    // }

    public void Show(Action onCloseButtonAction) {
        this.onCloseButtonAction = onCloseButtonAction;

        gameObject.SetActive(true);

        soundEffectsSlider.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}
