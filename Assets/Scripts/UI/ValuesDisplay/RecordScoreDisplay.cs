using System;
using TMPro;
using UnityEngine;

public class RecordScoreDisplay : MonoBehaviour
{
    [SerializeField] private RecordScoreSaver _saver;
    [SerializeField] private TMP_Text _valueDisplay;

    private void OnEnable() => Validate();

    private void Awake()
        => _valueDisplay.text = _saver.LoadRecordScore().ToString();

    private void Validate()
    {
        if (_saver == null)
            throw new InvalidOperationException();

        if (_valueDisplay == null)
            throw new InvalidOperationException();
    }
}