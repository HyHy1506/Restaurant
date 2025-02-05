using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut";
   [SerializeField] private CuttingCounter cuttingCounter;
   private Animator animator;
    public void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);

    }

 
}
