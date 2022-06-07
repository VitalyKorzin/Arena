using System;
using UnityEngine;

public class GamingTime : MonoBehaviour
{
    [SerializeField] private HeroSpawner _heroSpawner;

    private Hero _hero;

    private void OnEnable()
    {
        Validate();
        _heroSpawner.Spawned += OnHeroSpawned;
    }

    private void OnDisable() => _heroSpawner.Spawned -= OnHeroSpawned;

    public void Play() => Time.timeScale = 1;

    public void Pause() => Time.timeScale = 0;

    private void OnHeroSpawned(Hero hero)
    {
        _hero = hero != null ? hero : throw new InvalidOperationException();
        _hero.Died += OnHeroDied;
        Play();
    }

    private void OnHeroDied()
    {
        _hero.Died -= OnHeroDied;
        Pause();
    }

    private void Validate()
    {
        if (_heroSpawner == null)
            throw new InvalidOperationException();
    }
}