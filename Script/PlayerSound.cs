using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private float timerFootstep;
    private float timerFootstepMax=0.1f;

     private Player player;
    private void Awake()
    {
        player = GetComponent<Player>();
    }
    private void Start()
    {
        player = GetComponent<Player>();
    }
    private void Update()
    {
        timerFootstep -= Time.deltaTime;
        if(timerFootstep < 0)
        {
            timerFootstep = timerFootstepMax;
            if (player.IsWalking())
            {
                SoundManager.Instance.PlaySoundFootstep(player);
            }
        }
    }
}
