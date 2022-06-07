using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemiesSpawner : ObjectsPool<Enemy>
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Transform _enemiesContainer;
    [SerializeField] private HeroSpawner _heroSpawner;
    [SerializeField] private float _secondsBetweenSpawn;

    private Hero _hero;
    private uint _randomIndex;

    public event UnityAction<Enemy> Spawned;

    private void OnEnable() => _heroSpawner.Spawned += OnHeroSpawned;

    private void OnDisable() => _heroSpawner.Spawned -= OnHeroSpawned;

    protected override void Awake()
    {
        base.Awake();
        Initialize(_enemiesContainer);
    }

    protected override void Validate()
    {
        base.Validate();

        if (_enemiesContainer == null)
            throw new InvalidOperationException();

        if (_heroSpawner == null)
            throw new InvalidOperationException();

        if (_spawnPoints == null)
            throw new InvalidOperationException();

        if (_spawnPoints.Length == 0)
            throw new InvalidOperationException();

        foreach (var spawnPoint in _spawnPoints)
            if (spawnPoint == null)
                throw new InvalidOperationException();
    }

    private void OnHeroSpawned(Hero hero)
    {
        _hero = hero != null ? hero : throw new InvalidOperationException();
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var delay = new WaitForSeconds(_secondsBetweenSpawn);

        while (_hero.IsAlive && TryGetRandomObject(out Enemy enemy))
        {
            InitializeEnemy(enemy);
            Spawned?.Invoke(enemy);
            yield return delay;
        }
    }

    private void InitializeEnemy(Enemy enemy)
    {
        _randomIndex = (uint)UnityEngine.Random.Range(uint.MinValue, _spawnPoints.Length - 1);
        enemy.transform.position = _spawnPoints[_randomIndex].position;
        enemy.Activate();
        enemy.Initialize(_hero);
    }
}