using System;
using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;

    private void OnEnable() => Validate();

    private void Awake()
    {
        _weapon = Instantiate(_weapon, transform);
        _weapon.Initialize(null);
    }

    public IEnumerator Shoot() => _weapon.Shoot();

    public void TakeAim(float aimingAngle) 
        => _weapon.TakeAim(aimingAngle);

    private void Validate()
    {
        if (_weapon == null)
            throw new InvalidOperationException();
    }
}