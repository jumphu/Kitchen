using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        //Debug.Log($"ClearCounter.Interact called on {name}. HasKitchenObject={HasKitchenObject()}, player.HasKitchenObject={player.HasKitchenObject()}");

        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
               // Debug.Log("ClearCounter.Interact: Player has no kitchen object to give.");
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
               // Debug.Log("ClearCounter.Interact: Player already has a kitchen object, cannot take another.");
            }
            else
            {
                
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}