using UnityEngine;

public class HeroSkinShop : SkinShop<HeroSkin, HeroSkinShopItem>
{
    protected override void SetSelectedSkinInArenaLoader(HeroSkin selectedSkin)
         => ArenaLoader.SetHeroSkin(selectedSkin);
}