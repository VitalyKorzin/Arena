using System;
using UnityEngine;

public abstract class Shop<TGoods, TShopItem> : MonoBehaviour
    where TGoods : Goods
    where TShopItem : ShopItem<TGoods>
{
    [SerializeField] private TGoods[] _goods;
    [SerializeField] private TShopItem _shopItemTemplate;
    [SerializeField] private Transform _container;

    [SerializeField] protected Buyer Buyer;
    [SerializeField] protected GoodsSaver Saver;

    protected virtual void OnEnable() => Validate();

    private void Awake()
    {
        for (var i = 0; i < _goods.Length; i++)
            CreateShopItem(_goods[i]);
    }

    protected virtual void AddShopItem(TShopItem shopItem) { }

    protected virtual void Validate()
    {
        if (_goods == null)
            throw new InvalidOperationException();

        if (_goods.Length == 0)
            throw new InvalidOperationException();

        foreach (var good in _goods)
            if (good == null)
                throw new InvalidOperationException();

        if (_shopItemTemplate == null)
            throw new InvalidOperationException();

        if (_container == null)
            throw new InvalidOperationException();

        if (Buyer == null)
            throw new InvalidOperationException();

        if (Saver == null)
            throw new InvalidOperationException();
    }

    private void CreateShopItem(TGoods goods)
    {
        var shopItem = Instantiate(_shopItemTemplate, _container);
        goods.Initialize(Saver);
        shopItem.Initialize(goods);
        shopItem.Render();
        AddShopItem(shopItem);
    }
}