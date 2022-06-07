using System;
using TMPro;
using UnityEngine;
using IJunior.TypedScenes;

public class RecordScoreDisplay : MonoBehaviour, ISceneLoadHandler<PlayerResult>
{
    [SerializeField] private RecordScoreSaver _saver;
    [SerializeField] private TMP_Text _valueDisplay;

    private void Awake()
    {
        Validate();
        _valueDisplay.text = _saver.LoadRecordScore().ToString();
    }

    public void OnSceneLoaded(PlayerResult argument)
    {
        if (argument.Score > _saver.LoadRecordScore())
        {
            _saver.SaveRecordScore((int)argument.Score);
            _valueDisplay.text = argument.Score.ToString();
        }
    }

    private void Validate()
    {
        if (_saver == null)
            throw new InvalidOperationException();

        if (_valueDisplay == null)
            throw new InvalidOperationException();
    }
}