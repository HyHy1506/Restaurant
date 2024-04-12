using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManage : MonoBehaviour
{
    public event EventHandler OnDeliverySuccess;
    public event EventHandler OnDeliveryFail;

    public event EventHandler OnSpawnRecipe;
    public event EventHandler OnDestroyRecipe;
    private List<RecipeSO> waittingRecipeList;
    [SerializeField]private RecipeListSO recipeSOList;
    private float spawnTimer;
    private float spawnTimerMax=4f;
    private int waittingRecipesMax = 4;
    private int numberSuccessfulRecipes=0;
    static public DeliveryManage instance { get; private set; }
    private void Awake()
    {
        instance = this;
        waittingRecipeList = new List<RecipeSO>();
    }
    private void Update()
    {
       
            spawnTimer -= Time.deltaTime;
            if(spawnTimer < 0 )
            {
                spawnTimer = spawnTimerMax;
                if (waittingRecipeList.Count < waittingRecipesMax)
                {
                    // spawn a new waitting recipe
                    RecipeSO recipeSO = Instantiate(recipeSOList.recipeListSO[UnityEngine.Random.Range(0,recipeSOList.recipeListSO.Count)]);
                    waittingRecipeList.Add(recipeSO);
                    OnSpawnRecipe?.Invoke(this,EventArgs.Empty);
                }

            }
        
    }
    // when player delivery a recipe
    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        // cycir through waitting recipe list
        for(int i = 0;i<waittingRecipeList.Count;i++)
        {
            //compare amount of ingredient each recipe
            if (waittingRecipeList[i].kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                int ingredientMax = plateKitchenObject.GetKitchenObjectSOList().Count;
                // cycir through ingredient in a waitting recipe 
                foreach (KitchenObjectSO waittingKitchenObjectSO in waittingRecipeList[i].kitchenObjectSOList)
                {
                    // cycir through ingredient in a delivery Recipe
                    foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (waittingKitchenObjectSO == kitchenObjectSO)
                        {
                            ingredientMax--;
                        }
                    }
                }
                // all ingredients are same
                if(ingredientMax == 0)
                {
                    numberSuccessfulRecipes++;
                    RecipeSO recipeSO = waittingRecipeList[i];
                    waittingRecipeList.RemoveAt(i);
                    Destroy(recipeSO);
                    OnDestroyRecipe?.Invoke(this, EventArgs.Empty);
                    OnDeliverySuccess?.Invoke(this, EventArgs.Empty);

                    return;
                }
            }
        }
        Debug.Log("not oke" );
        OnDeliveryFail?.Invoke(this, EventArgs.Empty);


    }
    public List<RecipeSO> GetWaittingRecipeList()
    {
        return waittingRecipeList;

    }
    public int GetNumberSuccessfulRecipes()
    { return numberSuccessfulRecipes; }
}
