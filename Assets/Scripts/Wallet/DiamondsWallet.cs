using UnityEngine;
using UnityEngine.Events;

public class DiamondsWallet : Wallet
{
    public override void OnSceneLoaded(PlayerResult argument)
    {
        Balance = LoadBalance();
        Replenish(argument.DiamondsCount);
    }

    protected override void Awake()
    {
        Balance = LoadBalance();
        base.Awake();
    }

    protected override void ChangeBalance(uint value, UnityAction<uint> monetaryTransaction)
    {
        base.ChangeBalance(value, monetaryTransaction);
        Saver.SaveDiamondsCount((int)Balance);
    }

    private uint LoadBalance()
        => (uint)Saver.LoadDiamondsCount();
}