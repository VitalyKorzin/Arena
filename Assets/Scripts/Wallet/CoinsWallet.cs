using UnityEngine;
using UnityEngine.Events;

public class CoinsWallet : Wallet
{
    protected override void Awake()
    {
        Balance = (uint)Saver.LoadCoinsCount();
        base.Awake();
    }

    protected override void ChangeBalance(uint value, UnityAction<uint> monetaryTransaction)
    {
        base.ChangeBalance(value, monetaryTransaction);
        Saver.SaveCoinsCount((int)Balance);
    }
}