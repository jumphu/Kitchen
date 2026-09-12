using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoveCounter : BaseCounter
{
    private enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FryingRecipesSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
    [SerializeField] private float fryingTimer;
    [SerializeField] private float burningTimer;

    private FryingRecipesSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;   
    private State state;

    private void Start()
    { 
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    
                    if (fryingRecipeSO != null && fryingTimer >= fryingRecipeSO.fryingTimerMax)
                    {
                        // 烹饪完成，替换为输出物品
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        
                        

                        state = State.Fried;
                        burningTimer = 0f;

                        burningRecipeSO = GetBurningRecipeSOWithInput(fryingRecipeSO.output);
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;

                    if (burningRecipeSO != null && burningTimer >= burningRecipeSO.burningTimerMax)
                    {
                        // 烹饪完成，替换为输出物品
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        state = State.Burned;
                        //fryingTimer = 0f;
                    }

                    break;
                case State.Burned:
                    break;
            }
        }
        else
        {

            // 没有物品时重置状态
            state = State.Idle;
            fryingTimer = 0f;
            fryingRecipeSO = null;
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // 台面没有物品，玩家放置物品
            if (player.HasKitchenObject())
            {
                KitchenObject playerKO = player.GetKitchenObject();
                if (playerKO != null && HasRecipeWithInput(playerKO.GetKitchenObjectSO()))
                {
                    // 设置配方并开始烹饪
                    fryingRecipeSO = GetFryingRecipeSOWithInput(playerKO.GetKitchenObjectSO());
                    playerKO.SetKitchenObjectParent(this);
                    
                    state = State.Frying;
                    fryingTimer = 0f;
                }
            }
        }
        else
        {
            // 台面有物品，玩家拿取物品
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                
                // 重置状态
                
            }
            else
            {

                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
            }
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipesSO recipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (recipeSO != null)
        {
            return recipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipesSO recipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return recipeSO != null;
    }

    private FryingRecipesSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipesSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }
}
