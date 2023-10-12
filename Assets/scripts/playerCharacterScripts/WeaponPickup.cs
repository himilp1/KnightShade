using UnityEngine;
using TMPro;  // Import TMP namespace

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponToEquip; 
    public TextMeshProUGUI pickupPrompt; 

    private bool isPlayerInRange = false;

    private void Start()
    {
        pickupPrompt.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(isPlayerInRange)
        {
            if(Input.GetKeyDown(KeyCode.M))
            {
                AssignWeaponToSlot("Primary");
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                AssignWeaponToSlot("Secondary");
            }
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
            pickupPrompt.text = "Press M for Primary or N for Secondary to pick up " + weaponToEquip.name;
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
