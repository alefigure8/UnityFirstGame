
using System.Collections;
using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform getKitchenObjectFollowTransform();

    public void SetKitchenObject(kitchenObject kitchenObject);

    public kitchenObject GetKitchenObject();

    public void ClearKitchenObject();

    public bool HasKitchenObject();

}

