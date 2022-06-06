using UnityEngine;
using UnityEngine.Events;

public class DiamondsWallet : Wallet
{
    protected override void Awake()
    {
        Balance = (uint)Saver.LoadDiamondsCount();
        base.Awake();
    }

    protected override void ChangeBalance(uint value, UnityAction<uint> monetaryTransaction)
    {
        base.ChangeBalance(value, monetaryTransaction);
        Saver.SaveDiamondsCount((int)Balance);
    }
}