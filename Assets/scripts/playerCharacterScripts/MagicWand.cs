using UnityEngine;

public class MagicWand : MonoBehaviour
{
    public GameObject magicProjectile; // Assign your projectile prefab in the inspector
    public Transform firePoint; // A point from where the projectile will be instantiated

    public void FireMagic()
    {
        Instantiate(magicProjectile, firePoint.position, firePoint.rotation);
    }
}
