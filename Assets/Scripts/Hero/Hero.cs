using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Hero : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private uint _maximumHealth;

    private Animator _animator;
    private Health _health;
    private Weapon _weapon;
    private float _aimingAngle;

    public bool IsAlive => _health.CurrentValue != uint.MinValue;
    public uint MaximumHealth => _maximumHealth;

    public event UnityAction Died;
    public event UnityAction<uint> HealthChanged;

    private void Awake() => Validate();

    public void Initialize(Weapon weapon)
    {
        _weapon = weapon != null ? weapon : throw new InvalidOperationException();
        _animator = GetComponent<Animator>();
        _health = new Health(_maximumHealth);
    }

    public IEnumerator Shoot() => _weapon.Shoot();

    public void TakeAim(Vector2 direction)
    {
        Validate(direction);
        _aimingAngle = GetAngle(direction);
        _weapon.TakeAim(_aimingAngle);
        _animator.SetFloat(HeroAnimator.Params.AimingAngle, _aimingAngle);
        _animator.SetBool(HeroAnimator.Params.IsAiming, direction != Vector2.zero);
    }

    public void Move(Vector2 direction)
    {
        Validate(direction);
        transform.Translate(_movementSpeed * Time.deltaTime * direction);
        _animator.SetBool(HeroAnimator.Params.IsRunning, direction != Vector2.zero);
    }

    public void ApplyHit()
    {
        _health.DecreaseByOne();
        _animator.SetTrigger(HeroAnimator.Params.DamageReceived);
        NotifyOnHealtChanged();

        if (_health.CurrentValue == uint.MinValue)
            Died?.Invoke();
    }

    public void Heal()
    {
        _health.IncreaseByOne();
        _animator.SetTrigger(HeroAnimator.Params.Healed);
        NotifyOnHealtChanged();
    }

    private void NotifyOnHealtChanged() 
        => HealthChanged?.Invoke(_health.CurrentValue);

    private float GetAngle(Vector2 direction)
        => Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    private void Validate(Vector2 direction)
    {
        if (direction == null)
            throw new InvalidOperationException();
    }

    private void Validate()
    {
        if (_movementSpeed <= 0)
            throw new InvalidOperationException();

        if (_maximumHealth == uint.MinValue)
            throw new InvalidOperationException();
    }
}