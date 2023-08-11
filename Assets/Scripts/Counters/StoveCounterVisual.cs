using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour {
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesOnGameObject;
    [SerializeField] private StoveCounter stoveCounter;

    private void Start() {
        stoveCounter.OnStateChanged += StateChanged;
    }
    private void StateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        stoveOnGameObject.SetActive(showVisual);
        particlesOnGameObject.SetActive(showVisual);
    }

}
