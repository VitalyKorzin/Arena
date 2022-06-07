using UnityEngine;

public abstract class Skin : Goods
{
    [SerializeField] private bool _isBought;
    [SerializeField] private bool _isSelected;

    public bool IsBought => _isBought;
    public bool IsSelected => _isSelected;

    public void Select() => _isSelected = true;

    public void Deselect() => _isSelected = false;

    public override void Save(GoodsSaver saver)
    {
        var skinData = new SkinData
        {
            IsBought = _isBought,
            IsSelect = _isSelected
        };
        saver.SaveSkin(skinData, FileName);
    }

    public override void Initialize(GoodsSaver saver)
    {
        var skinData = saver.LoadSkin(FileName);
        _isBought = skinData.IsBought;
        _isSelected = skinData.IsSelect;
    }

    public override void Buy(Buyer buyer)
    {
        base.Buy(buyer);
        _isBought = true;
    }
}

[System.Serializable]
public class SkinData
{
    public bool IsBought;
    public bool IsSelect;
}