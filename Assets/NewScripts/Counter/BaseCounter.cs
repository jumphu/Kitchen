using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
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

    // 修复点：将 override 改为 virtual，使派生类可以 override 此签名
    public virtual void Interact(Player player)
    {
        if (kitchenObject == null)
        {
            //Transform KitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            //KitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
            //KitchenObjectTransform.localPosition = Vector3.zero;
        }
        else
        {
            // Give kitchen object to player
            //kitchenObject.SetKitchenObjectParent(player);
        }
    }

    public virtual void InteractAlternative(Player player)
    {
        // Default implementation (can be empty)
    }
}
