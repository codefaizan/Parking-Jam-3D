using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsRewardManager: MonoBehaviour
{
    private static CoinsRewardManager Instance;
    public static CoinsRewardManager instance
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<CoinsRewardManager>();
            }
            return Instance;
        }
    }
    ////////////////////////////////////
    int coins;
    private void Start()
    {
        coins = PlayerPrefs.GetInt("coins", 0);
    }

    internal void UpdateCoinsCount(int reward)
    {
        coins += reward;
        UIController.instance.UpdateCoinsArea(coins);
        PlayerPrefs.SetInt("coins", coins);
    }

}
