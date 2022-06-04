using System;
using UnityEngine;

[RequireComponent(typeof(ObstaclesSeeker))]
public abstract class TransitionBecauseOfObstacles : Transition
{
    [SerializeField] protected float TransitionRange;

    protected ObstaclesSeeker ObstaclesSeeker;

    private void Awake()
        => ObstaclesSeeker = GetComponent<ObstaclesSeeker>();

    public override void Initialize(Hero target)
    {
        base.Initialize(target);
        ObstaclesSeeker.Initialize(target.transform);
    }

    protected override void Validate()
    {
        base.Validate();

        if (TransitionRange <= 0)
            throw new InvalidOperationException();
    }
}