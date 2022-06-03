using System;
using System.Collections;
using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField] private float _secondsBetweenShotOfBursts;
    [SerializeField] private uint _bulletsCountInBurst;

    private uint _bulletsFiredCount;
    private WaitForSeconds _delayBetweenShotOfBursts;

    public override void Initialize(Transform bulletsContainer)
    {
        _delayBetweenShotOfBursts = new WaitForSeconds(_secondsBetweenShotOfBursts);
        base.Initialize(bulletsContainer);
    }

    public override IEnumerator Shoot()
    {
        while (TryGetRandomObject(out Bullet _))
        {
            yield return DelayBetweenShots;
            StartCoroutine(ShootBurst());
        }
    }
    protected override void Validate()
    {
        base.Validate();

        if (_secondsBetweenShotOfBursts <= 0)
            throw new InvalidOperationException();

        if (_bulletsCountInBurst == 0)
            throw new InvalidOperationException();
    }


    private IEnumerator ShootBurst()
    {
        _bulletsFiredCount = 0;

        while (_bulletsFiredCount < _bulletsCountInBurst && TryGetRandomObject(out Bullet bullet))
        {
            ShootOnce(bullet, ShotPoint.rotation);
            _bulletsFiredCount++;
            yield return _delayBetweenShotOfBursts;
        }
    }
}