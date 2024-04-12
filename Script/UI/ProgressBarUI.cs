using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    private IHasProgress iHasProgress;
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject hasProgressObject;
    // Start is called before the first frame update
    void Start()
    {
        iHasProgress = hasProgressObject.GetComponent<IHasProgress>();
        if(iHasProgress==null) { Debug.LogError( hasProgressObject+"dont has iHasProgress"); }
        iHasProgress.OnProgressChange += IHasProgress_OnProgressChange;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void IHasProgress_OnProgressChange(object sender, IHasProgress.OnProgressChangeEventArgs e)
    {
        barImage.fillAmount = e.progressNomalize;
        if (e.progressNomalize == 0f || e.progressNomalize >= 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

  
    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
}
