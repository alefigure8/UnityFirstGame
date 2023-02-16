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

    public void DestroySelf()
    {
        // Delete the kitchen object Parent
        KitchenObjectParent.ClearKitchenObject();

        // Destroy object
        Destroy(gameObject);
    }

    public static kitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        kitchenObject kitchenObject = kitchenObjectTransform.GetComponent<kitchenObject>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }
}
