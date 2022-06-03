using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Weapon : ObjectsPool<Bullet>
{
    [SerializeField] private float _secondsBetweenShots;

    [SerializeField] protected Transform ShotPoint;

    private Animator _animator;

    protected WaitForSeconds DelayBetweenShots;

    public virtual new void Initialize(Transform bulletsContainer)
    {
        _animator = GetComponent<Animator>();
        DelayBetweenShots = new WaitForSeconds(_secondsBetweenShots);
        base.Initialize(bulletsContainer);
    }

    public void TakeAim(float aimingAngle)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, aimingAngle);
        _animator.SetFloat(WeaponAnimator.Params.AimingAngle, aimingAngle);
    }

    public abstract IEnumerator Shoot();

    protected void ShootOnce(Bullet bullet, Quaternion rotation)
    {
        bullet.transform.SetPositionAndRotation(ShotPoint.position, rotation);
        bullet.Activate();
        _animator.SetTrigger(WeaponAnimator.Params.Fired);
    }

    protected override void Validate()
    {
        base.Validate();

        if (_secondsBetweenShots <= 0)
            throw new InvalidOperationException();

        if (ShotPoint == null)
            throw new InvalidOperationException();
    }
}