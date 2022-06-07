using System;
using UnityEngine;

public class GoodsSaver : Saver
{
    public void SaveSkin(SkinData skin, string fileName)
        => SaveObject(skin, fileName);

    public void SaveBooster(BoosterUpgradeData boosterUpgrade, string fileName)
        => SaveObject(boosterUpgrade, fileName);

    public BoosterUpgradeData LoadBoosterUpgrade(string fileName)
        => LoadObject<BoosterUpgradeData>(fileName);

    public SkinData LoadSkin(string fileName)
        => LoadObject<SkinData>(fileName);

    private T LoadObject<T>(string fileName)
    {
        if (TryLoadObject(fileName, out T result))
            return result;
        else
            throw new InvalidOperationException();
    }
}