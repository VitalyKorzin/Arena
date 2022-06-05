using System;
using UnityEngine;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] private HeroSpawner _heroSpawner;

    private void OnEnable() 
        => _heroSpawner.Spawned += OnHeroSpawned;

    protected virtual void OnDisable() 
        => _heroSpawner.Spawned -= OnHeroSpawned;

    protected virtual void OnHeroSpawned(Hero hero)
    {
        if (hero == null)
            throw new InvalidOperationException();
    }

    protected virtual void Validate()
    {
        if (_heroSpawner == null)
            throw new InvalidOperationException();
    }
}