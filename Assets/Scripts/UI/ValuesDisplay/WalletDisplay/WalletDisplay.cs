using System;
using TMPro;
using UnityEngine;

public abstract class WalletDisplay<TWallet> : MonoBehaviour
    where TWallet: Wallet
{
    [SerializeField] private TWallet _wallet;
    [SerializeField] private TMP_Text _valueDisplay;

    private void OnEnable()
    {
        Validate();
        _wallet.BalanceChanged += OnBalanceChanged;
    }

    private void OnDisable() 
        => _wallet.BalanceChanged -= OnBalanceChanged;

    private void OnBalanceChanged(uint balance)
        => _valueDisplay.text = balance.ToString();

    private void Validate()
    {
        if (_wallet == null)
            throw new InvalidOperationException();

        if (_valueDisplay == null)
            throw new InvalidOperationException();
    }
}