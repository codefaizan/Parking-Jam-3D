using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] internal Levels levels;
    int currLevel;
    public int carsCount;
    public static GameController instance;

    private void Start()
    {
        instance = this;
        currLevel = PlayerPrefs.GetInt("currLevel", 1);
        UIController.instance.UpdateLevelInfo(currLevel);
        SpawnLevel();
    }

    void SpawnLevel()
    {
        Instantiate(levels.environments[Random.Range(0, levels.environments.Length)]);
        Instantiate(levels.levelDetails[currLevel-1].levelPrefab);
        carsCount = GameObject.FindGameObjectsWithTag("Car").Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            carsCount--;
            if (carsCount == 0)
            {
                OnLevelComplete();
            }
        }
    }

    void OnLevelComplete()
    {
        UIController.instance.EnableResultView();
        UIController.instance.UpdateReward(levels.levelDetails[currLevel-1].coinsReward);
        CoinsRewardManager.instance.UpdateCoinsCount(levels.levelDetails[currLevel-1].coinsReward);
        
        currLevel++;
        PlayerPrefs.SetInt("currLevel", currLevel);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
