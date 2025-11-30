using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCreateUI : MonoBehaviour {




    [SerializeField] private Button createLobby;

    [SerializeField] private TMP_InputField lobbyNameInputField;

    [SerializeField] private UISwitcher.UISwitcher privateToggleSwitch;



    private void Awake() {
        
        createLobby.onClick.AddListener(() => {
            KitchenGameLobby.Instance.CreateLobby(lobbyNameInputField.text, privateToggleSwitch.isOn);
        });

    }




    public void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}