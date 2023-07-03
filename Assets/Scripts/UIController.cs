using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private static UIController Instance;
    public static UIController instance
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<UIController>();
            }
            return Instance;
        }
    }
    ///////////////
    [SerializeField] GameObject gameWinPanel;
    [SerializeField] Text gameLevelText;
    [SerializeField] Text completedLevelText;
    [SerializeField] Text coinsAreaText;
    [SerializeField] Text rewardText;
    [SerializeField] GameObject splashScreen;

    private void Start()
    {
        gameWinPanel.SetActive(false);
        
    }
    internal void ShowWinPanel()
    {
        gameWinPanel.SetActive(true);
    }

    internal void UpdateLevelInfo(int level)
    {
        gameLevelText.text += level.ToString();
        completedLevelText.text += level.ToString();
    }

    internal void UpdateReward(int reward)
    {
        rewardText.text = (int.Parse(rewardText.text) + reward).ToString();
    }

    internal void UpdateCoinsArea(int coins)
    {
        coinsAreaText.text = (int.Parse(coinsAreaText.text) + coins).ToString();
    }// called in reward manager
}
