using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    private static RewardManager Instance;
    public static RewardManager instance
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<RewardManager>();
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
