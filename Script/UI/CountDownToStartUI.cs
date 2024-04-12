using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownToStartUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCountDown;
    
    
    private void Start()
    {
        gameObject.SetActive(false);
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }
    private void Update()
    {
        textCountDown.text=Mathf.Ceil(GameManager.Instance.GetTimerCountDownToStart()).ToString();
    }
    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if(GameManager.Instance.IsCountDownToStart())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
       gameObject.SetActive (true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}