using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipesSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public  float fryingTimerMax = 3f;
}