using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;


public class WaypointsHolder : MonoBehaviour
{
    public PathCreator pathCreator;
    private static WaypointsHolder Instance;
    public static WaypointsHolder instance
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<WaypointsHolder>();
            }
            return Instance;
        }
    }





}
