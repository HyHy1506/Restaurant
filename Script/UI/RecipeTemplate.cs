using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeTemplate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconIngredient;
    private void Awake()
    {
        iconIngredient.gameObject.SetActive(false);
    }
    public void SetRecipeTemplate(RecipeSO recipeSO)
    {
        textMeshProUGUI.SetText(recipeSO.recipeName);
        foreach (Transform child in iconContainer)
        {
            if (child != iconIngredient)
            {
                Destroy(child.gameObject);
            }
        }
        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.GetKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(iconIngredient, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;

        }
    }
   
}
