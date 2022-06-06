using System;
using UnityEngine;
using IJunior.TypedScenes;

public class ArenaLoader : MonoBehaviour
{
    private HeroSkin _heroSkin;
    private WeaponSkin _weaponSkin;

    public void Load()
    {
        PlayerConfiguration playerConfig = new PlayerConfiguration()
        {
            HeroSkin = _heroSkin,
            WeaponSkin = _weaponSkin
        };
        Arena.Load(playerConfig);
    }

    public void SetHeroSkin(HeroSkin heroSkin) 
        => _heroSkin = heroSkin != null ? heroSkin : throw new InvalidOperationException();

    public void SetWeaponSkin(WeaponSkin weaponSkin)
        => _weaponSkin = weaponSkin != null ? weaponSkin : throw new InvalidOperationException();
}

public class PlayerConfiguration
{
    public HeroSkin HeroSkin;
    public WeaponSkin WeaponSkin;
}