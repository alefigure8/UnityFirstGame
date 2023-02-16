using System;
using System.Collections;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // There is not a kitchen object on the counter
            if (player.HasKitchenObject())
            {
                //player has a kitchen object With Recipes
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
        }
        else
        {
            // There is a kitchen object on the counter
            if (player.HasKitchenObject())
            {
                // player has a kitchen object
            }
            else
            {
                // If there is a kitchen object on the counter and the player does not have a kitchen object
                // Change the kitchen object
                GetKitchenObject().SetKitchenObjectParent(player);

            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            if (!player.HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                if(outputKitchenObjectSO != null)
                {
                    // Destroy the kitchen object
                    GetKitchenObject().DestroySelf();

                    // Create a new Object and set the parent
                    kitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
                }
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == kitchenObjectSO)
            {
                return true;
            }
        }

        return false;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if(cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }

}
