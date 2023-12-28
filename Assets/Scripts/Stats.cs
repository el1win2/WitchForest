using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StatsChangeType
{
    Add,
    Multiple,
    Override
}
public class Stats : MonoBehaviour
{
    public StatsChangeType StatChanType;
    public float Point;
}
