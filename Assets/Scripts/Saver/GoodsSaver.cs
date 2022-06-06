using System;
using UnityEngine;

public class GoodsSaver : Saver
{
    public void SaveSkin(SkinData skin, string pathToFile)
        => SaveObject(skin, pathToFile);

    public void SaveBooster(BoosterUpgradeData boosterUpgrade, string pathToFile)
        => SaveObject(boosterUpgrade, pathToFile);

    public BoosterUpgradeData LoadBoosterUpgrade(string pathToFile)
        => LoadObject<BoosterUpgradeData>(pathToFile);

    public SkinData LoadSkin(string pathToFile)
        => LoadObject<SkinData>(pathToFile);

    private T LoadObject<T>(string pathToFile)
    {
        if (TryLoadObject(pathToFile, out T result))
            return result;
        else
            throw new InvalidOperationException();
    }
}