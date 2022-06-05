using System;
using TMPro;
using UnityEngine;

public class CoinsCollectedCountDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _valueDisplay;
    [SerializeField] private HeroSpawner _heroSpawner;

    private RewardsCollector _collector;

    private void OnEnable()
    {
        Validate();
        _heroSpawner.Spawned += OnHeroSpawned;
    }

    private void OnDisable()
    {
        _heroSpawner.Spawned -= OnHeroSpawned;
        _collector.CoinsCountChanged -= OnCoinsCountChanged;
    }

    private void OnHeroSpawned(Hero hero)
    {
        if (hero == null)
            throw new InvalidOperationException();

        if (hero.gameObject.TryGetComponent(out RewardsCollector collector))
        {
            _collector = collector;
            _collector.CoinsCountChanged += OnCoinsCountChanged;
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    private void OnCoinsCountChanged(uint value)
        => _valueDisplay.text = value.ToString();

    private void Validate()
    {
        if (_valueDisplay == null)
            throw new InvalidOperationException();

        if (_heroSpawner == null)
            throw new InvalidOperationException();
    }
}