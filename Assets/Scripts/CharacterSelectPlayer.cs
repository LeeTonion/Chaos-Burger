using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectPlayer : MonoBehaviour {


    [SerializeField] private int playerIndex;
    [SerializeField] private GameObject readyGameObject;
    [SerializeField] private PlayerVisual playerVisual;
    [SerializeField] private Button kickButton;
    [SerializeField] private TextMeshPro playerNameText;

    [SerializeField] private Animator animator;

    private bool isOccupied = false;


    


    private void Awake() {
        kickButton.onClick.AddListener(() => {
            PlayerData playerData = KitchenGameMultiplayer.Instance.GetPlayerDataFromPlayerIndex(playerIndex);
            KitchenGameLobby.Instance.KickPlayer(playerData.playerId.ToString());
            KitchenGameMultiplayer.Instance.KickPlayer(playerData.clientId);
        });
    }

    private void Start() {

        Hide();
        KitchenGameMultiplayer.Instance.OnPlayerDataNetworkListChanged += KitchenGameMultiplayer_OnPlayerDataNetworkListChanged;
        CharacterSelectReady.Instance.OnReadyChanged += CharacterSelectReady_OnReadyChanged;

        kickButton.gameObject.SetActive(NetworkManager.Singleton.IsServer);

        UpdatePlayer();
    }

    private void CharacterSelectReady_OnReadyChanged(object sender, System.EventArgs e) {
        UpdatePlayer();
    }

    private void KitchenGameMultiplayer_OnPlayerDataNetworkListChanged(object sender, System.EventArgs e) {
        UpdatePlayer();
    }

    private void UpdatePlayer() {
    bool hasPlayer = KitchenGameMultiplayer.Instance.IsPlayerIndexConnected(playerIndex);

    if (hasPlayer) {

 
        if (!isOccupied) {
            Show();
        }

        isOccupied = true;

        PlayerData playerData = KitchenGameMultiplayer.Instance.GetPlayerDataFromPlayerIndex(playerIndex);

        readyGameObject.SetActive(CharacterSelectReady.Instance.IsPlayerReady(playerData.clientId));
        playerNameText.text = playerData.playerName.ToString();
        playerVisual.SetPlayerColor(
            KitchenGameMultiplayer.Instance.GetPlayerColor(playerData.colorId)
        );

    } else {

        if (isOccupied) {
            Hide();
        }

        isOccupied = false;
    }
}


    private void Show() {
        SetActiveRecursively(transform, true);
        animator.SetTrigger("show");
        
    }

    private void Hide() {
    SetActiveRecursively(transform, false);
}

private void SetActiveRecursively(Transform parent, bool state) {
    foreach (Transform child in parent) {
        child.gameObject.SetActive(state);
        SetActiveRecursively(child, state);
    }
}

    private void OnDestroy() {
        KitchenGameMultiplayer.Instance.OnPlayerDataNetworkListChanged -= KitchenGameMultiplayer_OnPlayerDataNetworkListChanged;
    }


}