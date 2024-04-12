using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private void Update()
    {
      
    }
    public override void Interact(Player player)
    {
        // counter has not kitchen object 
        if(!HasKitchenObject())
        {
            // player has kitchenobject
            if(player.HasKitchenObject())
            {
                // put kitchen ob from player to counter
                player.getKitchenObject().setKitchenObjectParent(this);
            }
        }
        // counter has  kitchen object 

        else
        {
            // player has not kitchenobject

            if (!player.HasKitchenObject())
            {
                // player get kitchen ob from  counter

                getKitchenObject().setKitchenObjectParent(player);

            }// player carring somethings
            else
            {
                //player carring a plate
                if (player.getKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(getKitchenObject().GetKitchenObjectSO()))
                    {

                        getKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    
                    //player carring something but not a plate 
                    if(getKitchenObject().TryGetPlate(out plateKitchenObject)) {
                    // counter has a plate
                    if(plateKitchenObject.TryAddIngredient(player.getKitchenObject().GetKitchenObjectSO()))
                        {
                            player.getKitchenObject().DestroySelf();
                        }
                    }
                }
            }
        }
    }
   
}
