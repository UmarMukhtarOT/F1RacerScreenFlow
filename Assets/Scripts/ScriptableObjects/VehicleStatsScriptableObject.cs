using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VehicleStats", menuName = "ScriptableObjects/VehicleStats", order = 51)]
public class VehicleStatsScriptableObject : ScriptableObject
{





    [Space(10)]
    public bool IsLocked;
    [Space(10)]

    [Range(0, 100)]
    public float speed;

    [Range(0, 10)]
    public float acceleration;

    [Range(0, 10)]
    public float boost;

    public int Price;
    public string vehicleName;
    public GameObject PlayerObject;






}
