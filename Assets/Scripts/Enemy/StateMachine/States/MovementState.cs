using UnityEngine;

[RequireComponent(typeof(Mover))]
public class MovementState : State
{
    private Mover _mover;

    protected override void OnEnable()
    {
        base.OnEnable();
        _mover.Initialize(Target.transform);
        Animator.SetBool(EnemyAnimator.Params.IsRunning, true);
    }

    private void OnDisable() 
        => Animator.SetBool(EnemyAnimator.Params.IsRunning, false);

    protected override void Awake()
    {
        base.Awake();
        _mover = GetComponent<Mover>();
    }

    protected virtual void Update()
    {
        _mover.Move();
        Animator.SetFloat(EnemyAnimator.Params.RunningAngle, GetAngle(_mover.CurrentDirection));
    }
}