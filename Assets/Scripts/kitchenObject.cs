using System.Collections;
using UnityEngine;

public class kitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO KitchenObjectSO;
    private ClearCounter clearCounter;
    
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return KitchenObjectSO;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        if(this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }
        this.clearCounter = clearCounter;
        
        if(clearCounter.HasKitchenObject())
        {
            Debug.LogError("Counter already has a KitchenObject");
        }
        
        clearCounter.SetKitchenObject(this);
        transform.parent = clearCounter.getKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter()
    {
        return this.clearCounter;
    }
}
