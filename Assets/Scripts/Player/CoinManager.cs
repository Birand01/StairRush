using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public delegate void OnCoinCollectHandler(int coin);
    public static event OnCoinCollectHandler OnCoinsCollect;

    public int totalCoinAmount;
    public int CoinCount { get;  set; } = 0;
    public static CoinManager Instance { get; private set; }
    private void Awake()
    {
     
        totalCoinAmount = CoinCount;
       if(Instance==null)
            Instance=this; 
    }
    private void Update()
    {
        totalCoinAmount = CoinCount;
    }
   
    public void AddCoin(int numberOfCoins)
    {
        CoinCount += numberOfCoins;
        PlayerPrefs.SetInt("Gold", CoinCount);
        OnCoinsCollect.Invoke(CoinCount);
    }
}
