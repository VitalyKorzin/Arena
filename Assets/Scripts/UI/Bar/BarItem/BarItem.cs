using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class BarItem : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private float _fillingDurationDefault;
    [SerializeField] private float _emptyingDurationDefault;

    private readonly uint _maximumFillingValue = 1;
    private readonly uint _minimumFillingValue = 0;
    private Coroutine _fillingJob;
    private float _elapsedTime;
    private float _nextValue;

    private void OnEnable() => Validate();

    public void Fill() => Fill(_fillingDurationDefault);

    public void Fill(float duration)
        => StartFill(_minimumFillingValue, _maximumFillingValue, duration, FillToEnd);

    public void Empty() => Empty(_emptyingDurationDefault);

    public void Empty(float duration) 
        => StartFill(_maximumFillingValue, _minimumFillingValue, duration, Destroy);

    public void Destroy() => Destroy(gameObject);

    private void StartFill(uint startValue, uint endValue, float duration, UnityAction lerpingEnd)
    {
        if (duration < 0)
            throw new InvalidOperationException();

        if (_fillingJob != null)
            StopCoroutine(_fillingJob);

        _fillingJob = StartCoroutine(Fill(startValue, endValue, duration, lerpingEnd));
    }

    private IEnumerator Fill(uint startValue, uint endValue, float duration, UnityAction lerpingEnd)
    {
        _elapsedTime = 0f;

        while (_elapsedTime < duration)
        {
            _nextValue = Mathf.Lerp(startValue, endValue, _elapsedTime / duration);
            _icon.fillAmount = _nextValue;
            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        lerpingEnd?.Invoke();
    }

    private void FillToEnd() => _icon.fillAmount = _maximumFillingValue;

    private void Validate()
    {
        if (_icon == null)
            throw new InvalidOperationException();

        if (_fillingDurationDefault < 0)
            throw new InvalidOperationException();

        if (_emptyingDurationDefault < 0)
            throw new InvalidOperationException();
    }
}