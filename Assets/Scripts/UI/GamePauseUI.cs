﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour {

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;

    private void Awake() {
        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionsButton.onClick.AddListener(() => {
            OptionUI.Instance.Show();
        });
    }


    private void Start() {
        GameManager.Instance.OnGamePaused += GamePaused;
        GameManager.Instance.OnGameUnpaused += GameUnpaused;

        Hide();
    }

    private void GamePaused(object sender, System.EventArgs e) {
        Show();
    }
    
    private void GameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }
    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }

}
