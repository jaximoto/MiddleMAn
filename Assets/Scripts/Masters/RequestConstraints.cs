using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuildingConstraints
{
    public static float MaxBuildTime = 30f;
    public static int MaxCost = 10000;
}



public class RequestWeigths
{
    public float time;
    public float money;
    public float importance;

    public RequestWeigths(float time, float money, float importance)
    {
        this.time = time;
        this.money = money;
        this.importance = importance;
    }
}

public class SchedulingParams
{
    public int currentDay;
    public int maxDeadline;

    public SchedulingParams(int currentDay, int maxDeadlineDay)
    {
        this.currentDay = currentDay;
        this.maxDeadline = maxDeadlineDay;
    }
}
