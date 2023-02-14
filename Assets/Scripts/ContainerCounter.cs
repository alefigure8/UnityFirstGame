using System;
using System.Collections;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnPlayerGrabbedObject;

    public override void Interact(Player player)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        kitchenObjectTransform.GetComponent<kitchenObject>().SetKitchenObjectParent(player);
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }

   
}
