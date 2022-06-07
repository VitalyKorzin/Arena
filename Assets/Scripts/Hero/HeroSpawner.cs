using System;
using UnityEngine;
using UnityEngine.Events;
using IJunior.TypedScenes;

public class HeroSpawner : MonoBehaviour, ISceneLoadHandler<PlayerConfiguration>
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _bulletsContainer;

    private Hero _hero;
    private Weapon _weapon;

    public event UnityAction<Hero> Spawned;

    private void Start() => Spawned?.Invoke(_hero);

    public void OnSceneLoaded(PlayerConfiguration argument)
    {
        Validate();
        _hero = Instantiate(argument.HeroSkin.Hero, _spawnPoint);
        _weapon = Instantiate(argument.WeaponSkin.Weapon, _hero.transform);
        _weapon.Initialize(_bulletsContainer);
        _hero.Initialize(_weapon);
    }

    private void Validate()
    {
        if (_spawnPoint == null)
            throw new InvalidOperationException();

        if (_bulletsContainer == null)
            throw new InvalidOperationException();
    }
}