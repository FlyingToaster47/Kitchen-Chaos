using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent {

    public event EventHandler OnPickedSomething;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    private KitchenObject kitchenObject;
 

    public static Player Instance { get; private  set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField] private InputController playerController;


    private bool isWalking = false;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;

    private void Awake() {
        if (Instance != null) {
            Debug.Log("Error: There is more than one Player Instance");
        }
        Instance = this;
    }


    private void Start() {
        // subscribing to the event
        playerController.OnInteractAction += InputControllerOnInteractAction;
        playerController.OnInteractAlternateAction += InputControllerOnInteractAlternateAction;
    }


    private void InputControllerOnInteractAction(object sender, System.EventArgs e) {
        if (!GameManager.Instance.IsGamePlaying()) return;
        
        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    private void InputControllerOnInteractAlternateAction (object sender, System.EventArgs e) {
        if (!GameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null) {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void Update() {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void HandleInteractions() {
        Vector2 inputVector = playerController.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), lastInteractDir, out RaycastHit raycastHit, interactDistance)) {
            // better way to get objects rather then using tags
            BaseCounter baseCounter = raycastHit.transform.GetComponent<BaseCounter>();
            if (baseCounter != null) {
                if (baseCounter != selectedCounter) {
                    SetSelectedCounter(baseCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement() {
         Vector2 inputVector = playerController.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        isWalking = moveDir != Vector3.zero;

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;

        // point 1, point 2, radius, direction, distance
        // similar to ray cast but with with and height
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (canMove) {
            transform.position += moveDir * moveDistance;
        } else {
            // move in x direction
            Vector3 moveDirx = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirx, moveDistance);
            if (canMove) {
                transform.position += moveDirx * moveDistance;
            } else {
                // move in z direction
                Vector3 moveDirz = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirz, moveDistance);
                if (canMove) {
                    transform.position += moveDirz * moveDistance;
                }
            }
        }

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter) {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {selectedCounter = selectedCounter});
    }

    public Transform GetKitchenObjectFollowTransform() {
        return kitchenObjectHoldPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null) {
            OnPickedSomething?.Invoke(this,EventArgs.Empty);
        }

    }
    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }
    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
