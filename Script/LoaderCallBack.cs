using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
    private bool isFristFrame = true;
    private void Update()
    {
        if (isFristFrame)
        {
            isFristFrame=false;
            Loader.LoaderCallBack();
        }
    }
}
