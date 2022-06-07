using System;
using UnityEngine;

public class Buyer : MonoBehaviour
{
    [SerializeField] private CoinsWallet _coinsWallet;
    [SerializeField] private DiamondsWallet _diamondsWallet;

    private void Awake() => Validate();

    public bool CheckSolvencyInCoins(uint price)
        => _coinsWallet.CheckSolvency(price);

    public bool CheckSolvencyInDiamonds(uint price)
        => _diamondsWallet.CheckSolvency(price);

    public void BuyPerCoins(uint price)
        => _coinsWallet.Withdraw(price);

    public void BuyPerDiamonds(uint price)
        => _diamondsWallet.Withdraw(price);

    private void Validate()
    {
        if (_coinsWallet == null)
            throw new InvalidOperationException();

        if (_diamondsWallet == null)
            throw new InvalidOperationException();
    }
}