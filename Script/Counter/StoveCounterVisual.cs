using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] GameObject particle;
    [SerializeField] GameObject stoveOnVisual;
    [SerializeField] StoveCounter stoveCounter;
    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStageChanged;
    }

    private void StoveCounter_OnStageChanged(object sender, StoveCounter.OnStateChangedEventAgrs e)
    {
        bool isShow = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried ;
        
            stoveOnVisual.SetActive(isShow);
            particle.SetActive(isShow);

        
       
    }
}
