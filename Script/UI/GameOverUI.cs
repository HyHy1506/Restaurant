using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textNumberRecipesDeliveried;
     private void Start()
    {
        gameObject.SetActive(false);
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }
   
    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            textNumberRecipesDeliveried.text =DeliveryManage.instance.GetNumberSuccessfulRecipes().ToString();
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

}