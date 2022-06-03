using System;
using UnityEngine;

public class Health
{
    private readonly int _maximumValue;

    public Health(int maximumValue)
    {
        if (maximumValue <= 0)
            throw new InvalidOperationException();

        _maximumValue = maximumValue;
        CurrentValue = maximumValue;
    }

    public int CurrentValue { get; private set; }

    public void DecreaseByOne() 
        => CurrentValue = Mathf.Clamp(--CurrentValue, 0, _maximumValue);
    
    public void IncreaseByOne()
        => CurrentValue = Mathf.Clamp(++CurrentValue, 0, _maximumValue);
}