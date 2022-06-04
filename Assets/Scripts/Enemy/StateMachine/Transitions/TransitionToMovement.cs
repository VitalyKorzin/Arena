using System;
using UnityEngine;

public class TransitionToMovement : Transition
{
    [SerializeField] private float _transitionRange;

    protected override bool CanTransit()
        => DistanceToTarget > _transitionRange;

    protected override void Validate()
    {
        base.Validate();

        if (_transitionRange <= 0)
            throw new InvalidOperationException();
    }
}