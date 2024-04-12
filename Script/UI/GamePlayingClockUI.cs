using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image imageTimer;
    private void Update()
    {
        imageTimer.fillAmount = GameManager.Instance.GetTimerGamePlayingNomalized();
    }
}