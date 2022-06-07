using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class ShootingState : State
{
    private Shooter _shooter;
    private Coroutine _shootingJob;
    private float _aimingAngle;

    private void OnEnable()
    {
        _shootingJob = StartCoroutine(_shooter.Shoot());
        Animator.SetBool(EnemyAnimator.Params.IsShooting, true);
    }

    private void OnDisable()
    {
        if (_shootingJob != null)
            StopCoroutine(_shootingJob);

        Animator.SetBool(EnemyAnimator.Params.IsShooting, false);
    }

    protected override void Awake()
    {
        base.Awake();
        _shooter = GetComponent<Shooter>();
    }

    private void Update()
    {
        _aimingAngle = GetAngle(DirectionToTarget);
        _shooter.TakeAim(_aimingAngle);
        Animator.SetFloat(EnemyAnimator.Params.ShootingAngle, _aimingAngle);
    }
}