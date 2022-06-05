using System;
using TMPro;
using UnityEngine;

public class GameOverWindow : Window
{
    [SerializeField] private HeroSpawner _heroSpawner;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TMP_Text _scoreDisplay;
    [SerializeField] private TMP_Text _coinsCountDisplay;

    private RewardsCollector _collector;
    private Hero _hero;

    private void OnEnable()
    {
        Validate();
        _heroSpawner.Spawned += OnHeroSpawned;
    }

    private void OnDisable()
    {
        _hero.Died -= OnHeroDied;
        _heroSpawner.Spawned -= OnHeroSpawned;
    }

    private void OnHeroSpawned(Hero hero)
    {
        _hero = hero != null ? hero : throw new InvalidOperationException();
        _hero.Died += OnHeroDied;

        if (_hero.gameObject.TryGetComponent(out RewardsCollector collector))
            _collector = collector;
        else
            throw new InvalidOperationException();
    }

    private void OnHeroDied()
    {
        Open();
        _scoreDisplay.text = _scoreCounter.Value.ToString();
        _coinsCountDisplay.text = _collector.Coins.ToString();
    }

    private void Validate()
    {
        if (_heroSpawner == null)
            throw new InvalidOperationException();

        if (_scoreCounter == null)
            throw new InvalidOperationException();

        if (_scoreDisplay == null)
            throw new InvalidOperationException();

        if (_coinsCountDisplay == null)
            throw new InvalidOperationException();
    }
}