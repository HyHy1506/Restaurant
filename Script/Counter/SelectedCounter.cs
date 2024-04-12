using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{
     
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] virtualGameObjectArray;
    // Start is called before the first frame update
    void Start()
    {
        Player.Intance.OnSelectedCounter += Player_OnSelectedCounter;    
    }

    private void Player_OnSelectedCounter(object sender, Player.OnSelectedCounterArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            setShow();
        }
        else { setHide(); }
    }

    private void setHide()
    {
        foreach(GameObject virtualGameObject in virtualGameObjectArray)
        {

        virtualGameObject.SetActive(false);
        }
    }
    private void  setShow()
    {

        foreach (GameObject virtualGameObject in virtualGameObjectArray)
        {

            virtualGameObject.SetActive(true);
        }
    }
}
