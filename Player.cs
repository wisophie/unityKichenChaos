using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public ClearCounter selectedCounter;
    }
    [SerializeField] private float speed =7f;
    [SerializeField] private GameInput gameInput;
    Vector3 moveDir;
    private bool isWalking;
    CharacterController cc;
    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter;
    //Rigidbody rb;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        //rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();

    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.Interact();
        }
    }

    private void Update() {
        HandleMovement();
        HandleInteractions();
    }
    private void HandleInteractions() {
        Vector2 inputVector = gameInput.GetMovementVector();
        moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }
        float interactDistance = 2f;
        //射线互动物体
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance)) {
            if(raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) { //是否发现了clearCounter
                                                                                      //has clearCounter
                if (clearCounter != selectedCounter) {

                    SetSelectedCounter(clearCounter);
                }
            } else {
                SetSelectedCounter(null);
            }
        } else {

            SetSelectedCounter(null);
        }
    }
    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVector();
        moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        //transform.position += moveDir * Time.deltaTime * speed;
        //Debug.Log(moveDir);


        isWalking = moveDir != Vector3.zero;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
    private void FixedUpdate() {
        //rb.velocity = moveDir * speed;
        cc.Move(moveDir*Time.deltaTime * speed);
    }   
    public bool IsWalking() {
        return isWalking;
    }
    private void SetSelectedCounter(ClearCounter abcCounter) {
        this.selectedCounter = abcCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = abcCounter
        }) ;

        
    }

}