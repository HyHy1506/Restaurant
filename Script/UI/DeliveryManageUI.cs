using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManageUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;
    

    private List<Transform> recipesTemplateList;
    private void Awake()
    {
        recipesTemplateList = new List<Transform>();
        recipeTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        DeliveryManage.instance.OnSpawnRecipe += DeliveryUI_OnSpawnRecipe;
        DeliveryManage.instance.OnDestroyRecipe += DeliveryUI_OnDestroyRecipe;
    }

    private void DeliveryUI_OnDestroyRecipe(object sender, System.EventArgs e)
    {
        UpdateVisual();

    }

    private void DeliveryUI_OnSpawnRecipe(object sender, System.EventArgs e)
    {
        UpdateVisual();  
    }
    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) { continue; }
            Destroy(child.gameObject);
        }
        foreach (RecipeSO recipeSO in DeliveryManage.instance.GetWaittingRecipeList())
        {
            Transform recipeTranform = Instantiate(recipeTemplate, container);
            recipeTranform.gameObject.SetActive(true);
            recipeTranform.GetComponent<RecipeTemplate>().SetRecipeTemplate(recipeSO);

        }
    }
}
