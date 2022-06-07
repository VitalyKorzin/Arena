using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBar : Bar
{
    [SerializeField] private HeartBarItem _heartTemplate;

    private readonly List<HeartBarItem> _hearts = new List<HeartBarItem>();
    private Hero _hero;
    private HeartBarItem _lastHeart;
    private HeartBarItem _newHeart;

    protected override void OnDisable()
    {
        base.OnDisable();
        _hero.HealthChanged -= OnHealthChanged;
    }

    protected override void OnHeroSpawned(Hero hero)
    {
        base.OnHeroSpawned(hero);
        _hero = hero;
        _hero.HealthChanged += OnHealthChanged;
        OnHealthChanged(_hero.MaximumHealth);
    }

    protected override void Validate()
    {
        base.Validate();

        if (_heartTemplate == null)
            throw new InvalidOperationException();
    }

    private void OnHealthChanged(uint currentValue)
    {
        if (_hearts.Count < currentValue)
            DisplayHearts(currentValue - (uint)_hearts.Count, CreateHeart);
        else if (_hearts.Count > currentValue)
            DisplayHearts((uint)_hearts.Count - currentValue, DestroyHeart);
    }

    private void DisplayHearts(uint count, UnityAction display)
    {
        for (var i = 0; i < count; i++)
            display?.Invoke();
    }

    private void CreateHeart()
    {
        _newHeart = Instantiate(_heartTemplate, transform);
        _hearts.Add(_newHeart);
        _newHeart.Fill();
    }

    private void DestroyHeart()
    {
        _lastHeart = _hearts[_hearts.Count - 1];
        _hearts.Remove(_lastHeart);
        _lastHeart.Empty();
    }
}