using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted

    }
    [SerializeField] private Mode mode;
    private void LateUpdate()
    {
        switch( mode){ 
            case Mode.LookAt:
                {
                    transform.LookAt(Camera.main.transform);
                    break;
                }
            case Mode.LookAtInverted:
                {
                    Vector3 dirFromCamere=transform.position-Camera.main.transform.position;

                    Debug.Log("dir"+dirFromCamere);
                  

                    Debug.Log("pos" + transform.position);
                    Vector3 dir = transform.position + dirFromCamere;
                    Debug.Log("sum" +dir  +" camera"+ Camera.main.transform.position);
                    transform.LookAt(transform.position+dirFromCamere);
                    break;
                }
                case Mode.CameraForward:
                {
                    transform.forward= Camera.main.transform.forward;
                    break;
                }
                case Mode.CameraForwardInverted:
                {
                    transform.forward=-Camera.main.transform.forward;
                    break;
                }
        }
    }
}
