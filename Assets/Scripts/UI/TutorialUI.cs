using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour {


   
    [SerializeField] private Button ContinueBtn;



    private void Start() {
 
        KitchenGameManager.Instance.OnLocalPlayerReadyChanged += KitchenGameManager_OnLocalPlayerReadyChanged;
        ContinueBtn.onClick.AddListener(() => GameInput.Instance.TriggerInteractAction());

        Show();
    }

    private void KitchenGameManager_OnLocalPlayerReadyChanged(object sender, System.EventArgs e) {
        if (KitchenGameManager.Instance.IsLocalPlayerReady()) {
            Hide();
        }
    }



    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}