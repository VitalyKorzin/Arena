using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class SkinShopItem<TSkin> : ShopItem<TSkin>
    where TSkin: Skin
{
    [SerializeField] private Button _sellButton;
    [SerializeField] private Button _selectButton;

    public event UnityAction<TSkin> SellButtonClick;
    public event UnityAction<TSkin> SelectButtonClick;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnSellButtonClick);
        _selectButton.onClick.AddListener(OnSelectButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnSellButtonClick);
        _selectButton.onClick.RemoveListener(OnSelectButtonClick);
    }

    public override void Render()
    {
        base.Render();
        LockSellButton();
        ChangeSelectButtonInteractivity();
    }

    protected override void Validate()
    {
        base.Validate();

        if (_selectButton == null)
            throw new InvalidOperationException();

        if (_sellButton == null)
            throw new InvalidOperationException();
    }

    private void ChangeSelectButtonInteractivity()
    {
        if (Goods.IsBought == false)
            _selectButton.interactable = false;

        if (Goods.IsSelected)
            _selectButton.interactable = false;

        if (Goods.IsBought && Goods.IsSelected == false)
            _selectButton.interactable = true;
    }

    private void LockSellButton()
    {
        if (Goods.IsBought)
            _sellButton.interactable = false;
    }

    private void OnSellButtonClick()
        => SellButtonClick?.Invoke(Goods);

    private void OnSelectButtonClick()
        => SelectButtonClick?.Invoke(Goods);
}