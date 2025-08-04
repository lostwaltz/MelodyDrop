using System;
using Engine;
using UnityEngine;

[Serializable]
public class TimeSourceData
{
    public TimeType timeType; 
}

public class TimeSource : MonoBehaviour
{
    [SerializeField] private TimeSourceData data;
    public TimeSourceData Data => data;

    public float DeltaTime { get; private set; }
    public float ElapsedTime { get; private set; } = 0f;

    public void Initialize(TimeSourceData timeData)
    {
        data = timeData;
    }
    
    public void UpdateTimeSource()
    {
        DeltaTime = Time.unscaledDeltaTime;
        
        ElapsedTime += DeltaTime;
    }
}
