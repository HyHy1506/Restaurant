using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ContainerCountervisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
   [SerializeField] private ContainerCounter containerCounter;
   private Animator animator;
    public void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnVisual;
    }

    private void ContainerCounter_OnVisual(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
