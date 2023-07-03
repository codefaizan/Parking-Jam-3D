using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Levels : ScriptableObject
{
    public GameObject[] environments;
    public LevelDetails[] levelDetails;

}

[System.Serializable]
public class LevelDetails
{
    public GameObject levelPrefab;
    public int coinsReward;
}
