using System;
using UnityEngine;

public class WalletSaver : Saver
{
    private readonly string _coinsCount = "CoinsCount";
    private readonly string _diamondsCount = "DiamondsCount";

    public void SaveCoinsCount(int coinsCount)
        => SaveIntegerValue(_coinsCount, coinsCount);

    public void SaveDiamondsCount(int diamondsCount)
        => SaveIntegerValue(_diamondsCount, diamondsCount);

    public int LoadCoinsCount() 
        => TryLoadIntegerValue(_coinsCount);

    public int LoadDiamondsCount() 
        => TryLoadIntegerValue(_diamondsCount);

    private int TryLoadIntegerValue(string key)
    {
        if (TryLoadIntegerValue(key, out int result))
            return result;
        else
            throw new InvalidOperationException();
    }
}