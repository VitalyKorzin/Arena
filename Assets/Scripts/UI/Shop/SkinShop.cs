using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SkinShop<TSkin, TSkinShopItem> : Shop<TSkin, TSkinShopItem>
    where TSkin : Skin
    where TSkinShopItem : SkinShopItem<TSkin>
{
    [SerializeField] protected ArenaLoader ArenaLoader;

    private readonly List<TSkinShopItem> _shopItems = new List<TSkinShopItem>();
    private TSkin _selectedSkin;

    protected override void OnEnable()
    {
        base.OnEnable();

        for (var i = 0; i < _shopItems.Count; i++)
        {
            _shopItems[i].SelectButtonClick += OnSelectButtonClick;
            _shopItems[i].SellButtonClick += OnSellButtonClick;
        }
    }

    private void OnDisable()
    {
        for (var i = 0; i < _shopItems.Count; i++)
        {
            _shopItems[i].SelectButtonClick -= OnSelectButtonClick;
            _shopItems[i].SellButtonClick -= OnSellButtonClick;
        }
    }

    protected override void AddShopItem(TSkinShopItem shopItem)
    {
        if (shopItem.Goods.IsSelected)
        {
            _selectedSkin = shopItem.Goods;
            SetSelectedSkinInArenaLoader(_selectedSkin);
        }

        _shopItems.Add(shopItem);
    }

    protected abstract void SetSelectedSkinInArenaLoader(TSkin selectedSkin);

    private void OnSelectButtonClick(TSkin skin)
    {
        if (_selectedSkin != null)
        {
            _selectedSkin.Deselect();
            _selectedSkin.Save(Saver);
        }

        skin.Select();
        skin.Save(Saver);
        _shopItems.FirstOrDefault(item => item.Goods == skin).Render();
        _shopItems.FirstOrDefault(item => item.Goods == _selectedSkin).Render();
        _selectedSkin = skin;
        SetSelectedSkinInArenaLoader(_selectedSkin);
    }

    private void OnSellButtonClick(TSkin skin)
    {
        if (skin.CheckSolvency(Buyer))
        {
            skin.Buy(Buyer);
            skin.Save(Saver);
            OnSelectButtonClick(skin);
        }
    }

    protected override void Validate()
    {
        base.Validate();

        if (ArenaLoader == null)
            throw new InvalidOperationException();
    }
}