using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; }

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnGamePause;


    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetButtonDown("Fire2")) {
            OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetButtonDown("Cancel")) {
            OnGamePause?.Invoke(this, EventArgs.Empty);
        }

    }

    public Vector2 GetMovementVectorNormalized() {

        Vector2 inputVector = new Vector2(0, 0);
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        inputVector.x = horizontal;
        inputVector.y = vertical;
        
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
