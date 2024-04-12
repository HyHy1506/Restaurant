using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseCounter : MonoBehaviour,IKitChenObjectParent
{
    public static event EventHandler OnObjectPlace;

    // [SerializeField] private Transform topPointCounter;
    [SerializeField]  protected Transform topPointCounter;
    private KitchenObject kitchenObject;
    public static void ResetStatic()
    {
        OnObjectPlace = null;
    }
    public virtual void Interact(Player player)
    {
        Debug.Log("not overide basecounter");
    }
    public virtual void InteractAlternate() {
        Debug.LogWarning("basecounter.interactalternae");
    }
    public void clearKitchenObject() { 
        kitchenObject = null;
    }
    public void setKitchenObject(KitchenObject kitchenObject) { 
        this.kitchenObject = kitchenObject; 
        if(kitchenObject != null)
        {
            OnObjectPlace?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject getKitchenObject() { return kitchenObject; }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
    public Transform getTopPointClearCounter() { return topPointCounter; }
}
