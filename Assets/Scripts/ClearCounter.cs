using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ClearCounter : MonoBehaviour
{
    [SerializeField]private KitchenObjectSO kitchenObjectSO;
    [SerializeField]private Transform counterTopPoint;
    [SerializeField]private ClearCounter secondClearCounter;
    [SerializeField]private bool testing;

    private kitchenObject kitchenObject;
    
    private void Update()
    {
        if(testing && Input.GetKeyDown(KeyCode.T))
        {
            if(kitchenObject != null)
            {
                kitchenObject.SetClearCounter(secondClearCounter);
            }
        }
    }

    public void Interact()
    {
        if(!kitchenObject)
         {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<kitchenObject>().SetClearCounter(this);

            kitchenObject = kitchenObjectTransform.GetComponent<kitchenObject>();
            kitchenObject.SetClearCounter(this);
        }
        else
        {
            Debug.Log(kitchenObject.GetClearCounter());
        }
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