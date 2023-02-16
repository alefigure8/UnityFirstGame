using System.Collections;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;

    private kitchenObject kitchenObject;
    
    public virtual void Interact(Player player)
    {
        Debug.LogError("Nevev Invoke");
    }

    public virtual void InteractAlternate(Player player)
    {
        Debug.LogError("Nevev Invoke");

    }

    public Transform getKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetKitchenObject(kitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public kitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}