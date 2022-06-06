using UnityEngine;

public class WeaponSkinShop : SkinShop<WeaponSkin, WeaponSkinShopItem>
{
    protected override void SetSelectedSkinInArenaLoader(WeaponSkin selectedSkin)
        => ArenaLoader.SetWeaponSkin(selectedSkin);
}