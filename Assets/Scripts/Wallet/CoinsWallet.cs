using UnityEngine;
using UnityEngine.Events;

public class CoinsWallet : Wallet
{
    public override void OnSceneLoaded(PlayerResult argument)
    {
        Balance = LoadBalance();
        Replenish(argument.CoinsCount);
    }

    protected override void Awake()
    {
        Balance = LoadBalance();
        base.Awake();
    }

    protected override void ChangeBalance(uint value, UnityAction<uint> monetaryTransaction)
    {
        base.ChangeBalance(value, monetaryTransaction);
        Saver.SaveCoinsCount((int)Balance);
    }

    private uint LoadBalance() 
        => (uint)Saver.LoadCoinsCount();
}