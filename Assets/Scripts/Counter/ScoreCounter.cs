using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private HeroSpawner _heroSpawner;
    [SerializeField] private uint _increasingValue;
    [SerializeField] private float _secondsBetweenIncrements;

    private Coroutine _increasingScoreJob;
    private RewardsCollector _collector;
    private WaitForSeconds _delayBetweenIncrements;
    private Hero _hero;

    public uint Value { get; private set; }

    public event UnityAction<uint> ValueChanged;

    private void OnEnable() => _heroSpawner.Spawned += OnHeroSpawned;

    private void OnDisable()
    {
        _heroSpawner.Spawned -= OnHeroSpawned;
        _collector.PickedUpScoreMultiplier -= OnPickedUpScoreMultiplier;
    }

    private void Awake()
    {
        Validate();
        _delayBetweenIncrements = new WaitForSeconds(_secondsBetweenIncrements);
    }

    private void OnHeroSpawned(Hero hero)
    {
        _hero = hero != null ? hero : throw new InvalidOperationException();
        _collector = GetRewardsCollector();
        _collector.PickedUpScoreMultiplier += OnPickedUpScoreMultiplier;
        StartIncreasingScoreJob(IncreaseScore());
    }

    private RewardsCollector GetRewardsCollector()
    {
        if (_hero.gameObject.TryGetComponent(out RewardsCollector collector))
            return collector;
        else throw new InvalidOperationException();
    }

    private IEnumerator IncreaseScore()
    {
        while (_hero.IsAlive)
        {
            Value += _increasingValue;
            NotifyOnValueChanged();
            yield return _delayBetweenIncrements;
        }
    }

    private void OnPickedUpScoreMultiplier(float duration, uint scoreMultiplier)
    {
        Validate(duration, scoreMultiplier);
        StopIncreasingScoreJob();
        StartIncreasingScoreJob(IncreaseByMultipliedScore(duration, scoreMultiplier));
    }

    private IEnumerator IncreaseByMultipliedScore(float duration, uint scoreMultiplier)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += _secondsBetweenIncrements;
            Value += _increasingValue * scoreMultiplier;
            NotifyOnValueChanged();
            yield return _delayBetweenIncrements;
        }

        StartIncreasingScoreJob(IncreaseScore());
    }

    private void StopIncreasingScoreJob()
    {
        if (_increasingScoreJob != null)
            StopCoroutine(_increasingScoreJob);
    }

    private void StartIncreasingScoreJob(IEnumerator routine)
        => _increasingScoreJob = StartCoroutine(routine);

    private void NotifyOnValueChanged()
        => ValueChanged?.Invoke(Value);

    private void Validate(float duration, uint scoreMultiplier)
    {
        if (duration <= 0)
            throw new InvalidOperationException();

        if (scoreMultiplier == uint.MinValue)
            throw new InvalidOperationException();
    }

    private void Validate()
    {
        if (_heroSpawner == null)
            throw new InvalidOperationException();

        if (_increasingValue == uint.MinValue)
            throw new InvalidOperationException();

        if (_secondsBetweenIncrements <= 0)
            throw new InvalidOperationException();
    }
}