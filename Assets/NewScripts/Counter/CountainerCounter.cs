using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountainerCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    

    public event EventHandler OnPlayerGrabbedObject;




    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);


        }

    }
}
