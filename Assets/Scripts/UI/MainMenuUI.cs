using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {


    [SerializeField] private Button MultiPlayerButton;
    [SerializeField] private Button SinglePlayerButton;
    [SerializeField] private Button quitButton;


    private void Awake() {
        MultiPlayerButton.onClick.AddListener(() => {
            KitchenGameMultiplayer.playMultiplayer = true;
            Loader.Load(Loader.Scene.LobbyScene);
        });
        SinglePlayerButton.onClick.AddListener(() => {
            KitchenGameMultiplayer.playMultiplayer= false;
            Loader.Load(Loader.Scene.LobbyScene);
        });
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });

        Time.timeScale = 1f;
    }

}