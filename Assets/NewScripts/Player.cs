using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour , IKitchenObjectParent
{
    public static Player Instance { get; private set; }


    public event EventHandler <OnSelectedCounterChangedEventArgs> OnselectedCounterChanged ;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private bool isWalking;
    [SerializeField] private Vector3 lastInteractDir;
    [SerializeField] private float interactionDistance = 2f;

    // 新增：射线高度偏移（在 Inspector 可调）
    [SerializeField] private float raycastHeight = 0.5f;
    
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private BaseCounter SelectedCounter;
    private KitchenObject kitchenObject;

    // Start is called before the first frame update

    // Update is called once per frame
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }
    void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractionAlternativeAction += GameInput_OnInteractionAlternativeAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (SelectedCounter != null)
        {
            // 调用带父对象参数的 Interact，这样柜台可以把物品给玩家或接收玩家的物品
            SelectedCounter.Interact(this);
        }
    }

    private void GameInput_OnInteractionAlternativeAction(object sender, System.EventArgs e)
    {
        if (SelectedCounter != null)
        {
            SelectedCounter.InteractAlternative(this);
        }
    }
    void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetInputVectorNormalized();
        // 重新赋值
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        this.isWalking = moveDir != Vector3.zero;
        float moveDistance = speed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove && moveDir != Vector3.zero)
        {
            //Cannot move towards moveDir
            //Attempt only x
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                //可以只沿X轴移动
                moveDir = moveDirX;
            }
            else
            {
                //Cannot move only x
                //Attempt only z
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.x != 0 &&  !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    //可以只沿Z轴移动
                    moveDir = moveDirZ;
                }
                else
                {
                    //Cannot Move in any direction
                    moveDir = Vector3.zero;
                }
            }
        }

        // 统一的移动和旋转逻辑（在所有检测完成后）
        if (moveDir != Vector3.zero && canMove)
        {
            transform.position += moveDistance * moveDir;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
        }

    }

    private void HandleInteraction() { 
   

        Vector2 inputVector = gameInput.GetInputVectorNormalized();
        // 重新赋值
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        RaycastHit raycastHit;

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir.normalized;
            
        }

        // 抬高射线起点，避免击中地面或角色本身
        Vector3 rayOrigin = transform.position + Vector3.up * raycastHeight;
        Debug.DrawRay(rayOrigin, lastInteractDir * interactionDistance, Color.red);

        if (Physics.Raycast(rayOrigin, lastInteractDir, out raycastHit, interactionDistance, counterLayerMask))
        {
            //Debug.Log("We hit " + raycastHit.transform.name + " !");
            // 使用 GetComponentInParent 更稳健（碰撞体是子物体时也能找到挂在父物体上的 BaseCounter）
            if (raycastHit.transform.GetComponentInParent<BaseCounter>() is BaseCounter baseCounter)
            {
                //Has ClearCounter
                if (baseCounter != SelectedCounter)
                {
                    SetSelcetedCounter(baseCounter);
                }
                else { 
                    SetSelcetedCounter(null);
                }
            }
            else { 
                SetSelcetedCounter(null);
                
            }
        }
        else
        {
            // 未命中时清空选中（可选）
            SetSelcetedCounter(null);
        }
    }


    private void SetSelcetedCounter(BaseCounter selectedCounter)
    {
        this.SelectedCounter = selectedCounter;
        OnselectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = SelectedCounter
        });

    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}