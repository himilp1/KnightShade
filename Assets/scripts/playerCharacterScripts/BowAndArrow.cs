using UnityEngine;

public class BowAndArrow : Weapon
{
    public GameObject arrowPrefab;      // Drag your arrow prefab here in the Inspector.
    public Transform arrowSpawnPoint;   // Assign where the arrow will be instantiated from (typically the tip of the bow).
    public float shootingForce = 10f;   // How fast the arrow will be shot.

    public override void Use()
    {
        ShootArrow();
    }

    private void ShootArrow()
    {
        // Instantiate an arrow at the spawn point.
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity);

        // Use the forward direction of the spawn point to determine the direction to shoot.
        Vector3 shootingDirection = arrowSpawnPoint.forward;

        // Add force to the arrow to propel it forward.
        Rigidbody arrowRb = arrow.GetComponent<Rigidbody>();
        if (arrowRb)
        {
            arrowRb.AddForce(shootingDirection * shootingForce, ForceMode.Impulse);
        }

        // Destroy the arrow after some time to prevent memory issues.
        Destroy(arrow, 5f);
    }
}
