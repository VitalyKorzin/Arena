using System.Collections;
using UnityEngine;

public class Pistol : Weapon
{
    public override IEnumerator Shoot()
    {
        while (TryGetRandomObject(out Bullet bullet))
        {
            yield return DelayBetweenShots;
            ShootOnce(bullet, ShotPoint.rotation);
        }
    }
}