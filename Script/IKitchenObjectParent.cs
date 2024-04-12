using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitChenObjectParent

{
    public void clearKitchenObject();
    public void setKitchenObject(KitchenObject kitchenObject);
    public KitchenObject getKitchenObject();
    public bool HasKitchenObject();
    public Transform getTopPointClearCounter();
}