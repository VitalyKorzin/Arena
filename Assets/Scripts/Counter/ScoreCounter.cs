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

    private void OnEnable()
    {
        Validate();
        _heroSpawner.Spawned += OnHeroSpawned;
    }

    private void OnDisable()
    {
        _heroSpawner.Spawned -= OnHeroSpawned;
        _collector.PickedUpScoreMultiplier -= OnPickedUpScoreMultiplier;
    }

    private void Awake()
        => _delayBetweenIncrements = new WaitForSeconds(_secondsBetweenIncrements);

    private void OnHeroSpawned(Hero hero)
    {
        _hero = hero != null ? hero : throw new InvalidOperationException();

        if (_hero.gameObject.TryGetComponent(out RewardsCollector collector))
        {
            _collector = collector;
            _collector.PickedUpScoreMultiplier += OnPickedUpScoreMultiplier;
        }
        else
        {
            throw new InvalidOperationException();
        }

        _increasingScoreJob = StartCoroutine(IncreaseScore());
    }

    private IEnumerator IncreaseScore()
    {
        while (_hero.IsAlive)
        {
            Value += _increasingValue;
            ValueChanged?.Invoke(Value);
            yield return _delayBetweenIncrements;
        }
    }

    private void OnPickedUpScoreMultiplier(float duration, uint scoreMultiplier)
    {
        if (duration <= 0)
            throw new InvalidOperationException();

        if (scoreMultiplier == uint.MinValue)
            throw new InvalidOperationException();

        if (_increasingScoreJob != null)
            StopCoroutine(_increasingScoreJob);

        _increasingScoreJob = StartCoroutine(IncreaseDoubleScore(duration, scoreMultiplier));
    }

    private IEnumerator IncreaseDoubleScore(float duration, uint scoreMultiplier)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += _secondsBetweenIncrements;
            Value += _increasingValue * scoreMultiplier;
            ValueChanged?.Invoke(Value);
            yield return _delayBetweenIncrements;
        }

        _increasingScoreJob = StartCoroutine(IncreaseScore());
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