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

    public event UnityAction<Enemy> Spawned;

    protected override void OnEnable()
    {
        base.OnEnable(); 
        _heroSpawner.Spawned += OnHeroSpawned;
    }

    private void OnDisable() => _heroSpawner.Spawned -= OnHeroSpawned;

    private void Awake() => Initialize(_enemiesContainer);

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
        int randomIndex;

        while (_hero.IsAlive && TryGetRandomObject(out Enemy enemy))
        {
            randomIndex = UnityEngine.Random.Range(0, _spawnPoints.Length - 1);
            enemy.transform.position = _spawnPoints[randomIndex].position;
            enemy.Activate();
            enemy.Initialize(_hero);
            Spawned?.Invoke(enemy);
            yield return delay;
        }
    }
}