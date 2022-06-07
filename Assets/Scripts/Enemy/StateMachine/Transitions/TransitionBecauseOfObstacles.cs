using System;
using UnityEngine;

[RequireComponent(typeof(ObstaclesSeeker))]
public abstract class TransitionBecauseOfObstacles : Transition
{
    [SerializeField] protected float TransitionRange;

    private ObstaclesSeeker _obstaclesSeeker;

    protected override void Awake()
    {
        base.Awake();
        _obstaclesSeeker = GetComponent<ObstaclesSeeker>();
    }

    protected override void Validate()
    {
        base.Validate();

        if (TransitionRange <= 0)
            throw new InvalidOperationException();
    }

    protected bool ThereAreNoObstacles()
        => _obstaclesSeeker.ThereAreNoObstacles(DirectionToTarget, DistanceToTarget);
}