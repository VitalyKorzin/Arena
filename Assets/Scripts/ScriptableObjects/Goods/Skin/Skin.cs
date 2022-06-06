using UnityEngine;

public abstract class Skin : Goods
{
    [SerializeField] private bool _isBought;
    [SerializeField] private bool _isSelected;

    public bool IsBought => _isBought;
    public bool IsSelected => _isSelected;

    public void Select() => _isSelected = true;

    public void Deselect() => _isSelected = false;

    public override void Buy(Buyer buyer)
    {
        base.Buy(buyer);
        _isBought = true;
    }
}