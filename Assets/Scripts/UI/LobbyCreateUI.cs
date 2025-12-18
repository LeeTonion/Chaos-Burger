using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCreateUI : MonoBehaviour {




    [SerializeField] private Button createLobby;

    [SerializeField] private TMP_InputField lobbyNameInputField;

    [SerializeField] private TMP_InputField lobbyTimeInputField;

    [SerializeField] private UISwitcher.UISwitcher privateToggleSwitch;

    [SerializeField] private Slider MaxPlayersSlider;

    private int MaxPlayers;



    private void Awake() {
        MaxPlayers = Mathf.RoundToInt(MaxPlayersSlider.value);
        MaxPlayersSlider.onValueChanged.AddListener(MaxPlayersSliderChanged);
        createLobby.onClick.AddListener(() => {
        int lobbyTime = int.Parse(lobbyTimeInputField.text); 
        KitchenGameLobby.Instance.CreateLobby(lobbyNameInputField.text,privateToggleSwitch.isOn,MaxPlayers,lobbyTime);
        });

    }

    private void MaxPlayersSliderChanged(float value)
    {
        MaxPlayers = Mathf.RoundToInt(value);
    }
    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    

}