using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter,IHasProgress
{
    public static event EventHandler OnAnyCut;
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProccess;
    public event EventHandler OnCut;
    public event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChange;
    new public static void ResetStatic()
    {
        OnAnyCut = null;
    }
    public class OnProgressChangeEventArgs : EventArgs
    {
        public float progressNomalize;
    }
    public override void Interact(Player player)
    {
        // counter has not kitchen object 
        if (!HasKitchenObject())
        {
            // player has kitchenobject
            if (player.HasKitchenObject())
            {
                if(HasCuttingRecipeSO(player.getKitchenObject().GetKitchenObjectSO()))
                    {
                    // put kitchen ob from player to counter
                     player.getKitchenObject().setKitchenObjectParent(this);

                    cuttingProccess = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(getKitchenObject().GetKitchenObjectSO());
                    OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
                    {
                        progressNomalize =(float) cuttingProccess / (float)cuttingRecipeSO.cuttingProgressMax
                    }) ;
                }
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

            }
            else
            {
                if (player.getKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(getKitchenObject().GetKitchenObjectSO()))
                    {

                        getKitchenObject().DestroySelf();
                    }
                }
            }
        }
    }
    public override void InteractAlternate()
    {
        if (HasKitchenObject() && HasCuttingRecipeSO(getKitchenObject().GetKitchenObjectSO()))
        {
            // has kitcheobj and has output
            cuttingProccess++;
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(getKitchenObject().GetKitchenObjectSO());
            OnCut?.Invoke(this,EventArgs.Empty);
            OnAnyCut?.Invoke(this, EventArgs.Empty);

            OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
            {
                progressNomalize = (float)cuttingProccess / (float)cuttingRecipeSO.cuttingProgressMax
            });
            if (cuttingProccess >=GetCuttingRecipeSO(getKitchenObject().GetKitchenObjectSO()).cuttingProgressMax) {
            
            KitchenObjectSO kitchenObjectSO_Output=GetKitchenObjectSO_Output(getKitchenObject().GetKitchenObjectSO());
                    getKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(kitchenObjectSO_Output, this);
            
            }

           
        }
    }
    private bool HasCuttingRecipeSO(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }
    private KitchenObjectSO GetKitchenObjectSO_Output(KitchenObjectSO inputKitchenObjectSO)
    {
        
        //
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(inputKitchenObjectSO);
        if (cuttingRecipeSO !=null)
        {
            return cuttingRecipeSO.output;
        }return null;
    }
    private CuttingRecipeSO GetCuttingRecipeSO(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipe in cuttingRecipeSOArray)
        {
            if (cuttingRecipe.input == inputKitchenObjectSO)
            {
                return cuttingRecipe;

            }
        }
        return null;
    }
}