using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event  EventHandler OnTrash;
    new public static void ResetStatic()
    {
        OnTrash = null;
    }
    public override void Interact(Player player)
    {
        if(player.HasKitchenObject())
        {
        player.getKitchenObject().DestroySelf();
            OnTrash?.Invoke(this,EventArgs.Empty);
        }
    }
}
