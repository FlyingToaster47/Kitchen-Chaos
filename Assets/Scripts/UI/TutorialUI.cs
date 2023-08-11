using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour {

    private void Start() {
        Show();
        GameManager.Instance.OnStateChanged += StateChanged;
    }

    private void StateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsCountdownToStartActive()) {
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
