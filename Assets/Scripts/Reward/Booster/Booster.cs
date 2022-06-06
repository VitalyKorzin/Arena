using System;
using UnityEngine;

public abstract class Booster : Reward
{
    [SerializeField] private float _duration;

    public float Duration => _duration;

    public void Initialize(float duration)
    {
        if (duration <= 0)
            throw new InvalidOperationException();

        _duration = duration;
    }
}