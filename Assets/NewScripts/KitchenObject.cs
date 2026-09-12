using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
   [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;
    public KitchenObjectSO GetKitchenObjectSO()
    {
         return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if (this.kitchenObjectParent!= null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent;
        //Debug.Log("Setting ClearCounter for KitchenObject on object: " + name);

        // 调试：打印目标 counter 的名字、HasKitchenObject 返回值和实际 GetKitchenObject
        //Debug.Log($"Target counter: {(clearCounter != null ? clearCounter.name : "null")}, HasKitchenObject() = {(clearCounter != null ? clearCounter.HasKitchenObject().ToString() : "N/A")}, GetKitchenObject() = {(clearCounter != null && clearCounter.GetKitchenObject() != null ? clearCounter.GetKitchenObject().name : "null")}");

        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Counter occupied");
        }

        kitchenObjectParent.SetKitchenObject(this);
        //Debug.Log($"Target counter: {(clearCounter != null ? clearCounter.name : "null")}, HasKitchenObject() = {(clearCounter != null ? clearCounter.HasKitchenObject().ToString() : "N/A")}, GetKitchenObject() = {(clearCounter != null && clearCounter.GetKitchenObject() != null ? clearCounter.GetKitchenObject().name : "null")}");


        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }


    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }


}
