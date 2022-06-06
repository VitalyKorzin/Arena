using UnityEngine;

[CreateAssetMenu(menuName = "Goods/Skin/Weapon", order = 51)]
public class WeaponSkin : Skin
{
    [SerializeField] private Weapon _Weapon;

    public Weapon Weapon => _Weapon;
}