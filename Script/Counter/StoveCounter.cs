using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter,IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangeEventArgs> OnProgressChange;
  
    public event EventHandler<OnStateChangedEventAgrs> OnStateChanged;
    public event EventHandler<OnStateChangedEventAgrs> OnStateChangedNotContinuous;

    public class OnStateChangedEventAgrs : EventArgs
    {
        public State state;
    }
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private float fryingTimer;
    private float burningTimer;

    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;
    private State state;
    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                {
                    break;
                }
            case State.Frying:
                {

                    OnStateChanged?.Invoke(this, new OnStateChangedEventAgrs
                    {
                        state = state
                    }) ;
                    fryingTimer += Time.deltaTime;
                    OnProgressChange?.Invoke(this,new IHasProgress.OnProgressChangeEventArgs { progressNomalize = fryingTimer / fryingRecipeSO.fryingTimerMax });
                    if (fryingTimer >= fryingRecipeSO.fryingTimerMax)
                    {
                        getKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output,this); 
                        state = State.Fried;
                        fryingTimer = 0f;
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSO(getKitchenObject().GetKitchenObjectSO());

                    }
                    break;
                }
            case State.Fried:
                {

                    OnStateChanged?.Invoke(this, new OnStateChangedEventAgrs
                    {
                        state = state
                    });
                    burningTimer += Time.deltaTime;
                    OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs { progressNomalize = burningTimer / burningRecipeSO.burningTimerMax });
                    if (burningTimer >= burningRecipeSO.burningTimerMax)
                    {
                        getKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                        state = State.Burned;
                        burningTimer = 0f;
                        OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs 
                        { progressNomalize = 0f });
                        OnStateChangedNotContinuous?.Invoke(this, new OnStateChangedEventAgrs
                        {
                            state = state
                        });
                    }
                    break;
                }
            case State.Burned:
                {
                    OnStateChanged?.Invoke(this, new OnStateChangedEventAgrs
                    {
                        state = state
                    });
                    break;
                }
        }
       
    }
    public override void Interact(Player player)
    {
        // counter has not kitchen object 
        if (!HasKitchenObject())
        {
            // player has kitchenobject
            if (player.HasKitchenObject())
            {
                if (HasFryingRecipeSO(player.getKitchenObject().GetKitchenObjectSO()))
                {
                    // put kitchen ob from player to counter
                    player.getKitchenObject().setKitchenObjectParent(this);
                    fryingTimer = 0;
                    state = State.Frying;
                    fryingRecipeSO=GetFryingRecipeSO(getKitchenObject().GetKitchenObjectSO());
                    OnStateChangedNotContinuous?.Invoke(this, new OnStateChangedEventAgrs
                    {
                        state = state
                    });
                    OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
                    { progressNomalize = 0f });

                    OnStateChanged?.Invoke(this, new OnStateChangedEventAgrs
                    {
                        state = state
                    });
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
                state = State.Idle;
                OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
                { progressNomalize = 0f });
                OnStateChanged?.Invoke(this, new OnStateChangedEventAgrs
                {
                    state = state
                });
                OnStateChangedNotContinuous?.Invoke(this, new OnStateChangedEventAgrs
                {
                    state = state
                });
            }
            else
            {
                // player has plate
                if (player.getKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(getKitchenObject().GetKitchenObjectSO()))
                    {

                        getKitchenObject().DestroySelf();
                        state = State.Idle;
                        OnProgressChange?.Invoke(this, new IHasProgress.OnProgressChangeEventArgs
                        { progressNomalize = 0f });
                        OnStateChanged?.Invoke(this, new OnStateChangedEventAgrs
                        {
                            state = state
                        });
                        OnStateChangedNotContinuous?.Invoke(this, new OnStateChangedEventAgrs
                        {
                            state = state
                        });
                    }
                }

            }
        }
    }
    private bool HasFryingRecipeSO(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO FryingRecipeSO = GetFryingRecipeSO(inputKitchenObjectSO);
        return FryingRecipeSO != null;
    }
    private KitchenObjectSO GetKitchenObjectSO_Output(KitchenObjectSO inputKitchenObjectSO)
    {

        //
        FryingRecipeSO FryingRecipeSO = GetFryingRecipeSO(inputKitchenObjectSO);
        if (FryingRecipeSO != null)
        {
            return FryingRecipeSO.output;
        }
        return null;
    }
    private FryingRecipeSO GetFryingRecipeSO(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipe in fryingRecipeSOArray)
        {
            if (fryingRecipe.input == inputKitchenObjectSO)
            {
                return fryingRecipe;

            }
        }
        return null;
    }
    private BurningRecipeSO GetBurningRecipeSO(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipe in burningRecipeSOArray)
        {
            if (burningRecipe.input == inputKitchenObjectSO)
            {
                return burningRecipe;

            }
        }
        return null;
    }

}