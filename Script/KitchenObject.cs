using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    private IKitChenObjectParent iKitChenObjectParent;
    
    public void setKitchenObjectParent(IKitChenObjectParent iKitChenObjectParent)
    {
        if(this.iKitChenObjectParent!=null) {
        this.iKitChenObjectParent.clearKitchenObject();
        }

        this.iKitChenObjectParent = iKitChenObjectParent;

        iKitChenObjectParent.setKitchenObject(this);
        FollowParent(iKitChenObjectParent.getTopPointClearCounter());
    }
    private void FollowParent(Transform parent)
    {
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
    }
    public IKitChenObjectParent GetKitchenObjectParent() { return iKitChenObjectParent; }
    //-----------
    public void DestroySelf()
    {
        iKitChenObjectParent.clearKitchenObject();
        Destroy(gameObject);
    }
    public static KitchenObject  SpawnKitchenObject(KitchenObjectSO inputKitchenObjectSO,IKitChenObjectParent kitChenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(inputKitchenObjectSO.prefab);
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.setKitchenObjectParent(kitChenObjectParent);
        return kitchenObject;
    }
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if(this is PlateKitchenObject)
        {
            plateKitchenObject=this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }
}
