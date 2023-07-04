using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MySO/Rewards")]
public class Rewards : ScriptableObject
{
    public GameObject[] puzzles;
    public GameObject carGiftCanvas;
    public Transform giftMoveTarget;
}
