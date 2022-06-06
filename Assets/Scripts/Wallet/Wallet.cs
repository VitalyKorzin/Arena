using UnityEngine;
using UnityEngine.Events;

public abstract class Wallet : MonoBehaviour
{
    protected uint Balance;

    public event UnityAction<uint> BalanceChanged;

    protected virtual void Awake()
        => NotifyOnBalanceChanged();

    public void Withdraw(uint price)
        => ChangeBalance(price, DecreaseBalance);

    public void Replenish(uint value)
        => ChangeBalance(value, IncreaseBalance);

    public bool CheckSolvency(uint price) => Balance >= price;

    protected virtual void ChangeBalance(uint value, UnityAction<uint> monetaryTransaction)
    {
        monetaryTransaction?.Invoke(value);
        NotifyOnBalanceChanged();
    }

    private void DecreaseBalance(uint value) => Balance -= value;

    private void IncreaseBalance(uint value) => Balance += value;

    private void NotifyOnBalanceChanged()
        => BalanceChanged?.Invoke(Balance);
}