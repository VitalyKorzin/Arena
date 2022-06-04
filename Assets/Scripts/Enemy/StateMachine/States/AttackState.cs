using System;
using System.Collections;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private float _secondsBetweenAttack;

    private WaitForSeconds _delayBetweenAttack;
    private Coroutine _attackingJob;

    protected override void OnEnable()
    {
        base.OnEnable();
        Animator.SetBool(EnemyAnimator.Params.IsAttacking, true);
        _attackingJob = StartCoroutine(Attack());
    }

    private void OnDisable()
    {
        if (_attackingJob != null)
            StopCoroutine(_attackingJob);

        Animator.SetBool(EnemyAnimator.Params.IsAttacking, false);
    }

    protected override void Awake()
    {
        base.Awake();
        _delayBetweenAttack = new WaitForSeconds(_secondsBetweenAttack);
    }

    protected override void Validate()
    {
        base.Validate();

        if (_secondsBetweenAttack <= 0)
            throw new InvalidOperationException();
    }

    private IEnumerator Attack()
    {
        while (Target.IsAlive)
        {
            Animator.SetFloat(EnemyAnimator.Params.AttackingAngle, GetAngle(DirectionToTarget));
            Animator.SetTrigger(EnemyAnimator.Params.HitApplied);
            yield return _delayBetweenAttack;
            Target.ApplyHit();
        }
    }
}