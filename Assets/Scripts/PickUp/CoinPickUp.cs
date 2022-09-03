using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : PickUp
{
    [SerializeField] int coinAmount;
    protected override void OnPickUp(PlayerMovement player)
    {
        CoinManager coinManager  = player.GetComponent<CoinManager>();
        if(coinManager != null)
        {
            coinManager.AddCoin(coinAmount);
            this.gameObject.SetActive(false);
        }
    }   
}
