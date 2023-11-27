using UnityEngine;
using TMPro;  // Import TMP namespace

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponToEquip;
    public TextMeshProUGUI pickupPrompt;
    public CanvasGroup interactTextBackground;

    public GameObject player;
    private PlayerPointsTracker playerPointsTracker;

    private bool isPlayerInRange = false;
    public int weaponCost = 0;

    private void Start()
    {
        playerPointsTracker = player.GetComponent<PlayerPointsTracker>();
        pickupPrompt.gameObject.SetActive(false);
        interactTextBackground.alpha = 0;
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            if (Input.GetKeyDown(KeyCode.Q) && playerPointsTracker.currentPoints >= weaponCost)
            {
                AssignWeaponToSlot("Primary");
                playerPointsTracker.SpendPoints(weaponCost);
                interactTextBackground.alpha = 0;
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
            Debug.Log("weaponToEquip: " + weaponToEquip.name);
            playerInv.AssignWeapon(weaponToEquip, slot);
            pickupPrompt.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactTextBackground.alpha = 1;
            pickupPrompt.gameObject.SetActive(true);
            pickupPrompt.text = "Press Q to Pickup";
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactTextBackground.alpha = 0;
            pickupPrompt.gameObject.SetActive(false);
            isPlayerInRange = false;
        }
    }
}
