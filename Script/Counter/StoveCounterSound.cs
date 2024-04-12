using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private StoveCounter StoveCounter;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {

        StoveCounter.OnStateChangedNotContinuous += StoveCounter_OnStateChangedNotContinuous;
    }

    private void StoveCounter_OnStateChangedNotContinuous(object sender, StoveCounter.OnStateChangedEventAgrs e)
    {
        bool playSound = (e.state == StoveCounter.State.Fried || e.state == StoveCounter.State.Frying);
        if(playSound) {
            Debug.Log("chiem");
            audioSource.Play();
        }
        else
        {
            Debug.Log("ko chiem");

            audioSource.Pause();
        }
    }
}
