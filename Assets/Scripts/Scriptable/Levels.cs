using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="MySO/Levels")]
public class Levels : ScriptableObject
{
    public GameObject[] environments;
    public LevelDetails[] levelDetails;
    public GameObject carGiftCanvas;

}

[System.Serializable]
public class LevelDetails
{
    public GameObject levelPrefab;
    public int coinsReward;
}
