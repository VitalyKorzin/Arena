using System;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    [SerializeField] private HeroSpawner _heroSpawner;
    [SerializeField] private float _movementSpeed;

    private Transform _target;
    private Vector3 _offset;
    private Vector3 _targetPosition;

    private void OnEnable()
    {
        Validate();
        _heroSpawner.Spawned += OnHeroSpawned;
    }

    private void OnDisable() => _heroSpawner.Spawned -= OnHeroSpawned;

    private void FixedUpdate()
    {
        if (_target == null)
            return;

        _targetPosition = _target.position - _offset;
        transform.position = Vector3.Lerp(transform.position, _targetPosition, _movementSpeed * Time.fixedDeltaTime);
    }

    private void OnHeroSpawned(Hero hero)
    {
        _target = hero != null ? hero.transform : throw new InvalidOperationException();
        _offset = _target.position - transform.position;
    }

    private void Validate()
    {
        if (_heroSpawner == null)
            throw new InvalidOperationException();

        if (_movementSpeed <= 0)
            throw new InvalidOperationException();
    }
}
