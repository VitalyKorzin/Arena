using System;
using UnityEngine;

public class RewardsDispenser : ObjectsPool<Reward>
{
    [SerializeField] private Transform _rewardsContainer;
    [SerializeField] private EnemiesSpawner _enemiesSpawner;

    private void OnEnable()
        => _enemiesSpawner.Spawned += OnEnemySpawned;

    private void OnDisable()
        => _enemiesSpawner.Spawned -= OnEnemySpawned;

    protected override void Awake()
    {
        base.Awake();
        Initialize(_rewardsContainer);
    }

    protected override void Validate()
    {
        base.Validate();

        if (_rewardsContainer == null)
            throw new InvalidOperationException();

        if (_enemiesSpawner == null)
            throw new InvalidOperationException();
    }

    private void OnEnemySpawned(Enemy enemy)
        => enemy.Died += OnEnemyDied;

    private void OnEnemyDied(Enemy enemy)
    {
        enemy.Died -= OnEnemyDied;
        Dispense(enemy.transform);
    }

    private void Dispense(Transform dispensePoint)
    {
        if (TryGetRandomObject(out Reward reward))
        {
            reward.transform.SetPositionAndRotation(dispensePoint.position, Quaternion.identity);
            reward.Activate();
        }
    }
}