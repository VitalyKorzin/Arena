using System;
using UnityEngine;

public class RecordScoreSaver : Saver
{
    private readonly string _recordScore = "RecordScore";

    public void SaveRecordScore(int recordScore)
        => SaveIntegerValue(_recordScore, recordScore);

    public int LoadRecordScore()
    {
        if (TryLoadIntegerValue(_recordScore, out int result))
            return result;
        else 
            throw new InvalidOperationException();
    }
}