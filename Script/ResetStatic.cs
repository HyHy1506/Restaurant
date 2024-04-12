using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStatic : MonoBehaviour
{
    private void Awake()
    {
        BaseCounter.ResetStatic();  
        CuttingCounter.ResetStatic();
        TrashCounter.ResetStatic();
    }
}
