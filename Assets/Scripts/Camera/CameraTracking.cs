using System;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    [SerializeField] private HeroSpawner _heroSpawner;
    [SerializeField] private float _movementSpeed;

    private Transform _target;
    private Vector3 _offset;

    private void OnEnable() 
        => _heroSpawner.Spawned += OnHeroSpawned;

    private void OnDisable() 
        => _heroSpawner.Spawned -= OnHeroSpawned;

    private void Awake() => Validate();

    private void FixedUpdate() => FollowTarget();

    private void OnHeroSpawned(Hero hero)
    {
        _target = hero != null ? hero.transform : throw new InvalidOperationException();
        _offset = _target.position - transform.position;
    }

    private void FollowTarget() 
        => transform.position = Vector3.Lerp(transform.position, GetTargetPosition(),
            _movementSpeed * Time.fixedDeltaTime);

    private Vector3 GetTargetPosition() => _target.position - _offset;

    private void Validate()
    {
        if (_heroSpawner == null)
            throw new InvalidOperationException();

        if (_movementSpeed <= 0)
            throw new InvalidOperationException();
    }
}
