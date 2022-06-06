using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopItem<TGoods> : MonoBehaviour
    where TGoods: Goods
{
    [SerializeField] private TMP_Text _labelDisplay;
    [SerializeField] private TMP_Text _priceDisplay;
    [SerializeField] private Image _iconCurrency;
    [SerializeField] private Image _icon;

    public TGoods Goods { get; private set; }

    protected virtual void OnEnable() => Validate();

    public void Initialize(TGoods goods)
        => Goods = goods ?? throw new InvalidOperationException();

    public virtual void Render()
    {
        _labelDisplay.text = Goods.Label;
        _priceDisplay.text = Goods.Price.ToString();
        _icon.sprite = Goods.Icon;
        _iconCurrency.sprite = Goods.IconCurrency;
    }

    protected virtual void Validate()
    {
        if (_labelDisplay == null)
            throw new InvalidOperationException();

        if (_priceDisplay == null)
            throw new InvalidOperationException();

        if (_iconCurrency == null)
            throw new InvalidOperationException();

        if (_icon == null)
            throw new InvalidOperationException();
    }
}