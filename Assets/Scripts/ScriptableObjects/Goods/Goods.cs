using UnityEngine;

public abstract class Goods : ScriptableObject
{
    [SerializeField] private string _fileName;
    [SerializeField] private Payment _payment;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _label;
    [SerializeField] private uint _price;

    public Sprite Icon => _icon;
    public string Label => _label;
    public Sprite IconCurrency => _payment.IconCurrency;
    public uint Price { get { return _price; } protected set { _price = value; } }

    protected string PathToFile => $"\'Save\'{_fileName}.json";

    public bool CheckSolvency(Buyer buyer)
        => _payment.CheckSolvency(Price, buyer);

    public virtual void Buy(Buyer buyer)
        => _payment.Buy(Price, buyer);
}