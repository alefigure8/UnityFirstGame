using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO  input;
    public KitchenObjectSO  output;
    public int cuttingProgressMax;
}
