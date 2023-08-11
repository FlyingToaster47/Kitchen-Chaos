using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveBarUI : MonoBehaviour {
    [SerializeField] private Image barImage;
    [SerializeField] private StoveCounter hasProgress;


    private void Start() {

        hasProgress.OnProgressChanged += ProgressChanged;

        barImage.fillAmount = 0f;

        Hide();
    }

    private void ProgressChanged(object sender, StoveCounter.OnProgressChangedEventArgs e) {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized == 1f) {
            Hide();
        } else {
            Show();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }

}
