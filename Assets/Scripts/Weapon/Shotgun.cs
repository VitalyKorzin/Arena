using System;
using System.Collections;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private uint _bulletsCountInShot;
    [SerializeField] private float _scatterRange;

    private Quaternion _bulletRotation;
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
            _rotationZ = ShotPoint.rotation.z + GetRandomRotation();
            _rotationW = ShotPoint.rotation.w + GetRandomRotation();
            _bulletRotation = new Quaternion(0f, 0f, _rotationZ, _rotationW);
            ShootOnce(bullet, _bulletRotation);
            _bulletsFiredCount++;
        }
    }

    private float GetRandomRotation() 
        => UnityEngine.Random.Range(-_scatterRange, _scatterRange);
}