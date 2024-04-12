using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateKitChenObjectSO;
    public event EventHandler OnRemovePlate;
    public event EventHandler OnSpawnPlate;
    private int plateAmountMax = 4;
    private int plateAmount = 0;
    private float plateTimerSpawn = 0f;
    private float plateTimerMax = 4f;
    private void Start()
    {
        plateAmount = 0;
    }
    private void Update()
    {
        if (plateAmount < plateAmountMax) { 
        plateTimerSpawn += Time.deltaTime;
        if (plateTimerSpawn > plateTimerMax ) {
            plateAmount++;
            plateTimerSpawn = 0f;
            OnSpawnPlate?.Invoke(this,EventArgs.Empty);
        }
        }
    }
    public override void Interact(Player player)
    {
        // counter has plates
        if (plateAmount>0)
        {
        
            // player has not kitchenobject

            if (!player.HasKitchenObject())
            {
                // player get plate ob from  counter
                KitchenObject.SpawnKitchenObject(plateKitChenObjectSO,player);
                OnRemovePlate?.Invoke(this, EventArgs.Empty);
                plateAmount--;

            }
        }
    }
}
