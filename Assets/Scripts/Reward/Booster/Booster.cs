using System;
using UnityEngine;

public abstract class Booster : Reward
{
    public float Duration { get; private set; }

    public void Initialize(float duration)
    {
        if (duration <= 0)
            throw new InvalidOperationException();

        Duration = duration;
    }
}