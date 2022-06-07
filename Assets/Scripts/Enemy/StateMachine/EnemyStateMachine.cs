using System;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private Hero _target;
    private State _currentState;

    private void Awake() => Validate();

    private void Update()
    {
        if (_currentState.TryGetNextState(out State nextState))
            Transit(nextState);
    }

    public void Initialize(Hero target)
    {
        _target = target != null ? target : throw new InvalidOperationException();
        Transit(_firstState);
    }

    public void Deactivate() => _currentState.Exit();

    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState != null ? nextState : throw new InvalidOperationException();
        _currentState.Enter(_target);
    }

    private void Validate()
    {
        if (_firstState == null)
            throw new InvalidOperationException();
    }
}