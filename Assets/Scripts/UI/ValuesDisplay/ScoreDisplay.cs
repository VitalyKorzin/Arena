using System;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TMP_Text _valueDisplay;

    private void OnEnable() 
        => _scoreCounter.ValueChanged += OnScoreChanged;

    private void OnDisable() 
        => _scoreCounter.ValueChanged -= OnScoreChanged;

    private void Awake() => Validate();

    private void OnScoreChanged(uint score)
        => _valueDisplay.text = score.ToString();

    private void Validate()
    {
        if (_scoreCounter == null)
            throw new InvalidOperationException();

        if (_valueDisplay == null)
            throw new InvalidOperationException();
    }
}