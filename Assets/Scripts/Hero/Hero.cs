using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Hero : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private int _maximumHealth;

    private Animator _animator;
    private Health _health;
    private Weapon _weapon;
    private float _aimingAngle;

    public bool IsAlive => _health.CurrentValue != 0;
    public int MaximumHealth => _maximumHealth;

    public event UnityAction Died;
    public event UnityAction<int> HealthChanged;

    private void OnEnable() => Validate();

    public void Initialize(Weapon weapon)
    {
        _weapon = weapon != null ? weapon : throw new InvalidOperationException();
        _animator = GetComponent<Animator>();
        _health = new Health(_maximumHealth);
    }

    public IEnumerator Shoot() => _weapon.Shoot();

    public void TakeAim(Vector2 direction)
    {
        ValidateVector(direction);
        _aimingAngle = GetAngle(direction);
        _weapon.TakeAim(_aimingAngle);
        _animator.SetFloat(HeroAnimator.Params.AimingAngle, _aimingAngle);
        _animator.SetBool(HeroAnimator.Params.IsAiming, direction != Vector2.zero);
    }

    public void Move(Vector2 direction)
    {
        ValidateVector(direction);
        transform.Translate(_movementSpeed * Time.deltaTime * direction);
        _animator.SetBool(HeroAnimator.Params.IsRunning, direction != Vector2.zero);
    }

    public void ApplyHit()
    {
        _health.DecreaseByOne();
        _animator.SetTrigger(HeroAnimator.Params.DamageReceived);
        NotifyOnHealtChanged();

        if (_health.CurrentValue == 0)
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

    private void ValidateVector(Vector2 vector)
    {
        if (vector == null)
            throw new InvalidOperationException();
    }

    private void Validate()
    {
        if (_movementSpeed <= 0)
            throw new InvalidOperationException();

        if (_maximumHealth <= 0)
            throw new InvalidOperationException();
    }
}