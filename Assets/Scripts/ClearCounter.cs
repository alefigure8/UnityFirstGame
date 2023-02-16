using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ClearCounter : BaseCounter
{
    [SerializeField]private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // There is not a kitchen object on the counter
            if(player.HasKitchenObject())
            {
                //player has a kitchen object
                player.GetKitchenObject().SetKitchenObjectParent(this);
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

                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}