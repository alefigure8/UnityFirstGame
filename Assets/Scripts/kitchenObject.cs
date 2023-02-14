using System.Collections;
using UnityEngine;

public class kitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO KitchenObjectSO;
    
    private IKitchenObjectParent KitchenObjectParent;
    
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return KitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if(this.KitchenObjectParent != null)
        {
            this.KitchenObjectParent.ClearKitchenObject();
        }
        this.KitchenObjectParent = kitchenObjectParent;
        
        if(kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("KitchenObjectParent already has a KitchenObject");
        }
        
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.getKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return this.KitchenObjectParent;
    }
}
