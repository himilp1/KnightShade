using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Weapon GameObjects")]
    public GameObject bowAndArrow;
    public GameObject doubleSword;
    public GameObject magicWand;
    public GameObject singleSword;
    public GameObject spear;
    public GameObject twoHandSword;

    public GameObject defaultPrimaryWeapon;  // OHS07_Sword
    public GameObject defaultSecondaryWeapon; // OHS02_Sword

    public GameObject currentMeleeWeapon;
    private GameObject currentRangedWeapon;
    private GameObject player;

    private void Start()
    {

        // Set default weapons
        currentMeleeWeapon = defaultPrimaryWeapon;
        currentRangedWeapon = defaultSecondaryWeapon;
        player = GameObject.FindGameObjectWithTag("Player");
        EquipWeapon(defaultPrimaryWeapon);
    }

    public void SetDefaultPrimaryWeapon(GameObject newWeapon)
    {
        defaultPrimaryWeapon = newWeapon;
    }

    public void AssignWeapon(GameObject newWeapon, string slot)
    {
        if (slot == "Primary")
        {
            defaultPrimaryWeapon = newWeapon;
            EquipWeapon(defaultPrimaryWeapon);
        }
        else if (slot == "Secondary")
        {
            defaultSecondaryWeapon = newWeapon;
            EquipWeapon(defaultSecondaryWeapon);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            EquipWeapon(defaultPrimaryWeapon);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            EquipWeapon(defaultSecondaryWeapon);
        }
    }

    public void EquipWeapon(GameObject newWeapon)
    {
        Debug.Log("Equipping: " + newWeapon.name);
        if (newWeapon == null)  // Check if the weapon is not null
        {
            Debug.LogWarning("Trying to equip a null weapon.");
            return;
        }
        if (IsRangedWeapon(newWeapon))
        {
            if (currentRangedWeapon && currentRangedWeapon != newWeapon)
            {
                currentRangedWeapon.SetActive(false);
            }
            newWeapon.SetActive(true);
            currentRangedWeapon = newWeapon;
        }
        else
        {
            if (currentMeleeWeapon && currentMeleeWeapon != newWeapon)
            {
                currentMeleeWeapon.SetActive(false);
            }
            newWeapon.SetActive(true);
            currentMeleeWeapon = newWeapon;
            int weaponType = newWeapon.GetComponent<WeaponStats>().weaponType;
            player.GetComponent<SetWeaponAnimations>().Set(weaponType);
            
        }

    }

    private bool IsRangedWeapon(GameObject weapon)
    {
        return weapon == bowAndArrow || weapon == magicWand;
    }

    private bool IsTwoHandedSword(GameObject weapon)
    {
        return weapon.name.StartsWith("THS");
    }
}
