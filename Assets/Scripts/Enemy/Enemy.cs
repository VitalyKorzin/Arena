using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator), typeof(BoxCollider2D), typeof(EnemyStateMachine))]
public class Enemy : PoolObject
{
    [SerializeField] private float _secondsBeforeDeath;

    private WaitForSeconds _delayBeforeDeath;
    private EnemyStateMachine _stateMachine;
    private BoxCollider2D _collider;
    private Animator _animator;

    public event UnityAction<Enemy> Died;

    private void OnEnable() => Validate();

    private void Awake()
    {
        _delayBeforeDeath = new WaitForSeconds(_secondsBeforeDeath);
        _stateMachine = GetComponent<EnemyStateMachine>();
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
    }

    public void Initialize(Hero target)
    {
        if (target == null)
            throw new InvalidOperationException();

        _animator.SetBool(EnemyAnimator.Params.IsAlive, true);
        _collider.enabled = true;
        _stateMachine.Initialize(target);
    }

    public void Kill() => StartCoroutine(Die());

    private IEnumerator Die()
    {
        _stateMachine.Deactivate();
        _animator.SetBool(EnemyAnimator.Params.IsAlive, false);
        _collider.enabled = false;
        yield return _delayBeforeDeath;
        Died?.Invoke(this);
        Deactivate();
    }

    private void Validate()
    {
        if (_secondsBeforeDeath <= 0)
            throw new InvalidOperationException();
    }
}