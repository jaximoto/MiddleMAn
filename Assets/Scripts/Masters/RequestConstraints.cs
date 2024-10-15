using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuildingConstraints
{
    public static float MaxBuildTime = 10000000f;
    public static int MaxCost = 100000000;
}

public class PlayerWorkerStats
{
    public int workers;
    public int productivity;

    public PlayerWorkerStats(int workers, int productivity)
    {
        this.workers = workers;
        this.productivity = productivity;
    }
}

public class RequestWeights
{
    public float time;
    public float money;
    public float importance;

    public RequestWeights(float time, float money, float importance)
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
