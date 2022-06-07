using System;
using System.Collections;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private uint _bulletsCountInShot;
    [SerializeField] private float _scatterRange;

    private uint _bulletsFiredCount;
    private float _rotationZ;
    private float _rotationW;

    public override IEnumerator Shoot()
    {
        while (TryGetRandomObject(out Bullet _))
        {
            yield return DelayBetweenShots;
            ShootScatter();
        }
    }

    protected override void Validate()
    {
        base.Validate();

        if (_bulletsCountInShot == uint.MinValue)
            throw new InvalidOperationException();

        if (_scatterRange == 0)
            throw new InvalidOperationException();
    }

    private void ShootScatter()
    {
        _bulletsFiredCount = uint.MinValue;

        while (_bulletsFiredCount < _bulletsCountInShot && TryGetRandomObject(out Bullet bullet))
        {
            ShootOnce(bullet, GetBulletRotation());
            _bulletsFiredCount++;
        }
    }

    private Quaternion GetBulletRotation()
    {
        _rotationZ = ShotPoint.rotation.z + GetRandomRotation();
        _rotationW = ShotPoint.rotation.w + GetRandomRotation();
        return new Quaternion(0f, 0f, _rotationZ, _rotationW);
    }

    private float GetRandomRotation() 
        => UnityEngine.Random.Range(-_scatterRange, _scatterRange);
}