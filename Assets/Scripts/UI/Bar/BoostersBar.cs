using System;
using UnityEngine;

public class BoostersBar : Bar
{
    [SerializeField] private MagnetBarItem _magnetTemplate;
    [SerializeField] private ScoreMultiplierBarItem _scoreMultiplierTemplate;

    private MagnetBarItem _activeMagnetDisplay;
    private ScoreMultiplierBarItem _activeScoreMultiplierDisplay;
    private RewardsCollector _collector;

    protected override void OnHeroSpawned(Hero hero)
    {
        base.OnHeroSpawned(hero);
        _collector = GetRewardsCollector(hero);
        _collector.PickedUpScoreMultiplier += OnPickedUpScoreMultiplier;
        _collector.PickedUpMagnet += OnPickedUpMagnet;
    }

    protected override void Validate()
    {
        base.Validate();

        if (_magnetTemplate == null)
            throw new InvalidOperationException();

        if (_scoreMultiplierTemplate == null)
            throw new InvalidOperationException();
    }

    private RewardsCollector GetRewardsCollector(Hero hero)
    {
        if (hero.gameObject.TryGetComponent(out RewardsCollector collector))
            return collector;
        else throw new InvalidOperationException();
    }

    private void OnPickedUpScoreMultiplier(float duration, uint scoreMultiplier)
        => DisplayPickedUpBooster(duration, _scoreMultiplierTemplate, ref _activeScoreMultiplierDisplay);

    private void OnPickedUpMagnet(float duration)
        => DisplayPickedUpBooster(duration, _magnetTemplate, ref _activeMagnetDisplay);

    private void DisplayPickedUpBooster<T>(float duration, T template, ref T activeBarItem)
        where T : BarItem
    {
        if (duration < 0)
            throw new InvalidOperationException();

        if (activeBarItem != null)
            activeBarItem.Destroy();

        activeBarItem = Instantiate(template, transform);
        activeBarItem.Empty(duration);
    }
}