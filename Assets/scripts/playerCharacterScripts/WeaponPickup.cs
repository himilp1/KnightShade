using UnityEngine;
using TMPro;  // Import TMP namespace

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponToEquip;
    public TextMeshProUGUI pickupPrompt;

    public GameObject player;
    private PlayerPointsTracker playerPointsTracker;

    private bool isPlayerInRange = false;
    public int weaponCost = 0;

    private void Start()
    {
        playerPointsTracker = player.GetComponent<PlayerPointsTracker>();
        pickupPrompt.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            if (Input.GetKeyDown(KeyCode.M) && playerPointsTracker.currentPoints >= weaponCost)
            {
                AssignWeaponToSlot("Primary");
                playerPointsTracker.SpendPoints(weaponCost);
            }
            /*
            else if (Input.GetKeyDown(KeyCode.N))
            {
                AssignWeaponToSlot("Secondary");
            }
            */
        }
    }

    private void AssignWeaponToSlot(string slot)
    {
        PlayerInventory playerInv = FindObjectOfType<PlayerInventory>();
        if (playerInv)
        {
            playerInv.AssignWeapon(weaponToEquip, slot);
            pickupPrompt.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickupPrompt.gameObject.SetActive(true);
            pickupPrompt.text = "Press M to Pickup";
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickupPrompt.gameObject.SetActive(false);
            isPlayerInRange = false;
        }
    }
}
