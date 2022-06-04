using System;
using UnityEngine;

public class TransitionToAttack : Transition
{
    [SerializeField] private float _transitionRange;
    [SerializeField] private float _rangeSpread;

    private void Start()
        => _transitionRange += UnityEngine.Random.Range(-_rangeSpread, _rangeSpread);

    protected override bool CanTransit()
        => DistanceToTarget < _transitionRange;

    protected override void Validate()
    {
        base.Validate();

        if (_transitionRange <= 0)
            throw new InvalidOperationException();

        if (_rangeSpread <= 0)
            throw new InvalidOperationException();
    }
}