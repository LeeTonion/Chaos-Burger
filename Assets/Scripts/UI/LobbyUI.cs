using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour {


    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button createLobbyButton;
    [SerializeField] private Button quickJoinButton;
    [SerializeField] private Button joinCodeButton;
    [SerializeField] private TMP_InputField joinCodeInputField;
    [SerializeField] private TMP_InputField playerNameInputField;
    [SerializeField] private LobbyCreateUI lobbyCreateUI;
    [SerializeField] private Transform lobbyContainer;
    [SerializeField] private Transform lobbyTemplate;


    private void Awake() {
        mainMenuButton.onClick.AddListener(() => {
            KitchenGameLobby.Instance.LeaveLobby();
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        createLobbyButton.onClick.AddListener(() => {
            lobbyCreateUI.Show();
        });
        quickJoinButton.onClick.AddListener(() => {
            KitchenGameLobby.Instance.QuickJoin();
        });
        joinCodeButton.onClick.AddListener(() => {
            KitchenGameLobby.Instance.JoinWithCode(joinCodeInputField.text);
        });

        lobbyTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        playerNameInputField.text = KitchenGameMultiplayer.Instance.GetPlayerName();
        playerNameInputField.onValueChanged.AddListener((string newText) => {
            KitchenGameMultiplayer.Instance.SetPlayerName(newText);
        });

        KitchenGameLobby.Instance.OnLobbyListChanged += KitchenGameLobby_OnLobbyListChanged;
        UpdateLobbyList(new List<Lobby>());
    }

    private void KitchenGameLobby_OnLobbyListChanged(object sender, KitchenGameLobby.OnLobbyListChangedEventArgs e) {
        UpdateLobbyList(e.lobbyList);
    }

    private void UpdateLobbyList(List<Lobby> lobbyList)
    {
        List<Transform> children = new List<Transform>();

        foreach (Transform child in lobbyContainer)
        {
            if (child != null && child != lobbyTemplate)
            {
                children.Add(child);
            }
        }

        foreach (Transform child in children)
        {
            Destroy(child.gameObject); // Chỉ hủy khi đảm bảo đối tượng không null
        }

        foreach (Lobby lobby in lobbyList)
        {
            Transform lobbyTransform = Instantiate(lobbyTemplate, lobbyContainer);
            lobbyTransform.gameObject.SetActive(true);
            lobbyTransform.GetComponent<LobbyListSingleUI>().SetLobby(lobby);
        }
    }
    private void OnDestroy()
    {
        KitchenGameLobby.Instance.OnLobbyListChanged -= KitchenGameLobby_OnLobbyListChanged;
    }


}