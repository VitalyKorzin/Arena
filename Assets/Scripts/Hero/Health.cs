using System;
using UnityEngine;

public class Health
{
    private readonly uint _maximumValue;

    public Health(uint maximumValue)
    {
        if (maximumValue == uint.MinValue)
            throw new InvalidOperationException();

        _maximumValue = maximumValue;
        CurrentValue = maximumValue;
    }

    public uint CurrentValue { get; private set; }

    public void DecreaseByOne() 
        => CurrentValue = (uint)Mathf.Clamp(--CurrentValue, uint.MinValue, _maximumValue);
    
    public void IncreaseByOne()
        => CurrentValue = (uint)Mathf.Clamp(++CurrentValue, uint.MinValue, _maximumValue);
}