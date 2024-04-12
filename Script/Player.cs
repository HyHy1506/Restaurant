using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour,IKitChenObjectParent
{
    public static Player Intance { get; private set; }
    public  event EventHandler OnObjectPickUp;

    public event EventHandler<OnSelectedCounterArgs> OnSelectedCounter;
    public class OnSelectedCounterArgs : EventArgs
    {
      public  BaseCounter selectedCounter;
    }
    [SerializeField] private float speedMove = 3f;
    [SerializeField] private float speedRotate =15f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform topPoint;
    private KitchenObject kitchenObject;
    private BaseCounter selectedCounter;
    private bool isWalking=false;
    private Vector3 lastMove;
    private void Awake()
    {
        if (Intance != null)
        {
            Debug.LogError("have more than 1 intance");
        }
        Intance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameInput.OnInteract += GameInput_OnInteract;
        gameInput.OnInteractAlternate += GameInput_OnInteractAlternate;

    }

    private void GameInput_OnInteractAlternate(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter!= null)
        {
            selectedCounter.InteractAlternate();
        }
    }

    private void Update()
    {
        HandleInteraction();
        
    }
    private void FixedUpdate()
    {
        HandleMove();


    }
   private void HandleInteraction()
    {
        Vector2 inputVector2 = gameInput.MoveInputNormalize();
        Vector3 inputVector3 = new Vector3(inputVector2.x, 0, inputVector2.y);
        float playerDistance = 1.5f;

        if (Physics.Raycast(transform.position, lastMove, out RaycastHit raycastHit, playerDistance, layerMask))
        {

            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (selectedCounter == null) SetSelectedCounter(baseCounter);
                //----------------
              


                //-----------
            }
         
            else {
                SetSelectedCounter(null);
            }

        }
        else
        {
            SetSelectedCounter(null);

        }

    }
    private void GameInput_OnInteract(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        float playerDistance = 1.5f;

        if (Physics.Raycast(transform.position, lastMove, out RaycastHit raycastHit, playerDistance, layerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter selectedCounter))
            {
                selectedCounter.Interact(this);
            }
        }
   
    }

    private void HandleMove()
    {
        Vector2 inputVector2 = gameInput.MoveInputNormalize();
        Vector3 inputVector3 = new Vector3(inputVector2.x, 0, inputVector2.y);
        if(inputVector3!=Vector3.zero) { lastMove = inputVector3; }
        RotationMove(inputVector3);
        // collider
        float playerDistance = speedMove * Time.deltaTime;
        float playerRadius = 0.5f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, inputVector3, playerDistance);
        DiagonalMove(ref canMove,ref inputVector3,playerHeight,playerRadius, playerDistance);
        if (canMove) {
            
        transform.position += inputVector3 * speedMove * Time.deltaTime;
        }
        // rotation by direct move
        // animation when move
         if(inputVector3==Vector3.zero) isWalking =false; else isWalking =true;
    }
    private void RotationMove(Vector3 inputVector3)
    {
        transform.forward = Vector3.Slerp(transform.forward, inputVector3, speedRotate * Time.deltaTime);
    }
    public bool IsWalking()
    {
        return isWalking ;
    }
    private void DiagonalMove(ref bool canMove,ref Vector3 inputVector3,float playerHeight,float playerRadius, float playerDistance)
    {
        if (!canMove)
        {

            //attemp dir x
            Vector3 inputVector3TempX = new Vector3(inputVector3.x, 0, 0).normalized;
            canMove = (inputVector3.x < -0.5f || inputVector3.x > 0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, inputVector3TempX, playerDistance);
            if (canMove)
            {
                inputVector3 = inputVector3TempX;
            }
            else
            {
                //attemp dir z
                Vector3 inputVector3TempZ = new Vector3(0, 0, inputVector3.z).normalized;

                canMove = (inputVector3.z < -0.5f|| inputVector3.z > 0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, inputVector3TempZ, playerDistance);
                if (canMove)
                {
                    inputVector3 = inputVector3TempZ;
                }
            }
        }
    }
    private void SetSelectedCounter(BaseCounter basecounter)
    {
       this.selectedCounter = basecounter;
        OnSelectedCounter?.Invoke(this, new OnSelectedCounterArgs { selectedCounter = selectedCounter });
    }
    public Transform GetTopPoint()
    {
        return topPoint;
    }
    public void clearKitchenObject() { kitchenObject = null; }
    public void setKitchenObject(KitchenObject kitchenObject) { this.kitchenObject = kitchenObject;

        if (kitchenObject != null) {
            OnObjectPickUp?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject getKitchenObject() { return kitchenObject; }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
    public Transform getTopPointClearCounter() { return topPoint; }
}
