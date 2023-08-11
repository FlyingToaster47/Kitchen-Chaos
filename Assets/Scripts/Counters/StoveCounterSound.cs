using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour {
   
   private AudioSource audioSource;
   private float warningSoundTimer;
   private bool playWarningSound;

   [SerializeField] private StoveCounter stoveCounter;

   private void Awake() {
        audioSource = GetComponent<AudioSource>();
   }

    private void Start() {
        stoveCounter.OnStateChanged += StateChanged;
        stoveCounter.OnProgressChanged += ProgressChanged;
    }

    private void ProgressChanged(object sender, StoveCounter.OnProgressChangedEventArgs e) {
        float burnShowProgressAmount = .5f;
        bool playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
        

    }

    private void StateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

        if (playSound) {
            audioSource.Play();
        } else {
            audioSource.Pause();
        }

    }

    private void Update() {
        if (playWarningSound) {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0f) {
                float warningSoundTimerMax = .2f;
                warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }


}
