using UnityEngine;

public abstract class Payment : ScriptableObject
{
    [SerializeField] private Sprite _iconCurrency;

    public Sprite IconCurrency => _iconCurrency;

    public abstract bool CheckSolvency(uint price, Buyer buyer);

    public abstract void Buy(uint price, Buyer buyer);
}