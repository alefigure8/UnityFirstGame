using System;
using System.Collections;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnPlayerGrabbedObject;

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject())
        {
            kitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }

   
}
