using System;
using UnityEngine;
using UnityEngine.Events;

public class HeroSpawner : MonoBehaviour
{
    [SerializeField] private Hero hero;
    [SerializeField] private Weapon weapon;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _bulletsContainer;

    public event UnityAction<Hero> Spawned;

    private void OnEnable() => Validate();

    private void Start()
    {
        hero = Instantiate(hero, _spawnPoint.position, Quaternion.identity);
        weapon = Instantiate(weapon, hero.transform);
        weapon.Initialize(_bulletsContainer);
        hero.Initialize(weapon);
        Spawned?.Invoke(hero);
    }

    private void Validate()
    {
        if (hero == null)
            throw new InvalidOperationException();

        if (weapon == null)
            throw new InvalidOperationException();

        if (_spawnPoint == null)
            throw new InvalidOperationException();

        if (_bulletsContainer == null)
            throw new InvalidOperationException();
    }
}