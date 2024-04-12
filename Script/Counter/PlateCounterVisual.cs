using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] private PlateCounter plateCounter;
    [SerializeField] private GameObject plateKitChenObjectSpawn;
    private List<GameObject> plateKitchenObjectsList;
    // Start is called before the first frame update
    private void Awake()
    {
        
        plateKitchenObjectsList=new List<GameObject> ();
    }
    void Start()
    {
        plateCounter.OnSpawnPlate += PlateCounter_OnSpawnPlate;
        plateCounter.OnRemovePlate += PlateCounter_OnRemovePlate;
    }

    private void PlateCounter_OnRemovePlate(object sender, System.EventArgs e)
    {
       
        GameObject plateKitChenObjectLast = plateKitchenObjectsList[plateKitchenObjectsList.Count - 1];

        plateKitchenObjectsList.Remove(plateKitChenObjectLast);

        Destroy(plateKitChenObjectLast);

    }

    private void PlateCounter_OnSpawnPlate(object sender, System.EventArgs e)
    {
        float highPlate=0.1f*plateKitchenObjectsList.Count;
        GameObject plateKitchenObject = Instantiate(plateKitChenObjectSpawn, plateCounter.getTopPointClearCounter());
        plateKitchenObject.transform.localPosition = new Vector3(0, 0 + highPlate, 0);
        plateKitchenObjectsList.Add(plateKitchenObject);
        
    }
}
