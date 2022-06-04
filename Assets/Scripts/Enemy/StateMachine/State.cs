using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class State : MonoBehaviour
{
    [SerializeField] private Transition[] _transitions;

    protected Hero Target { get; private set; }
    protected Animator Animator { get; private set; }
    protected Vector2 DirectionToTarget
        => (Target.transform.position - transform.position).normalized;

    protected virtual void OnEnable() => Validate();

    protected virtual void Awake() 
        => Animator = GetComponent<Animator>();

    public void Enter(Hero target)
    {
        Target = target != null ? target : throw new InvalidOperationException();

        if (enabled == false)
        {
            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.Initialize(target);
                transition.enabled = true;
            }
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
                transition.enabled = false;

            enabled = false;
        }
    }

    public bool TryGetNextState(out State nextState)
    {
        var readyTransition = _transitions.FirstOrDefault(transition => transition.NeedTransit);
        nextState = readyTransition != null ? readyTransition.TargetState : null;
        return nextState != null;
    }

    protected float GetAngle(Vector2 direction)
        => Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    protected virtual void Validate()
    {
        if (_transitions == null)
            throw new InvalidOperationException();

        if (_transitions.Length == 0)
            throw new InvalidOperationException();

        foreach (var transition in _transitions)
            if (transition == null)
                throw new InvalidOperationException();
    }
}