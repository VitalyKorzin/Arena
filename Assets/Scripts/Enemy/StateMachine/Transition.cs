using System;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected Hero Target { get; private set; }
    protected float DistanceToTarget
        => Vector2.Distance(transform.position, Target.transform.position);
    protected Vector2 DirectionToTarget 
        => (Target.transform.position - transform.position).normalized;

    public State TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    protected virtual void Awake() => Validate();

    private void Update()
    {
        if (CanTransit())
            NeedTransit = true;
    }

    protected abstract bool CanTransit();

    public virtual void Initialize(Hero target)
    {
        Target = target != null ? target : throw new InvalidOperationException();
        NeedTransit = false;
    }

    protected virtual void Validate()
    {
        if (_targetState == null)
            throw new InvalidOperationException();
    }
}