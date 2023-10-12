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

    private GameObject currentMeleeWeapon;
    private GameObject currentRangedWeapon;

    private Animator animator;  // For setting animation parameters

    private void Start()
    {
        animator = GetComponent<Animator>();  // Initialize the animator reference

        // Set default weapons
        currentMeleeWeapon = defaultPrimaryWeapon;
        currentRangedWeapon = defaultSecondaryWeapon;

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
        }

        SetAnimationsBasedOnWeapon(newWeapon);  // Adjust animations based on weapon type
    }

    private bool IsRangedWeapon(GameObject weapon)
    {
        return weapon == bowAndArrow || weapon == magicWand;
    }

    private bool IsTwoHandedSword(GameObject weapon)
    {
        return weapon.name.StartsWith("THS");
    }

    private void SetAnimationsBasedOnWeapon(GameObject weapon)
    {
        // Resetting all animation booleans
        animator.SetBool("IsUsingTwoHandedSword", false);
        animator.SetBool("IsUsingMagicWand", false);
        // ... add other animation booleans resets here ...

        if (IsTwoHandedSword(weapon))
        {
            animator.SetBool("IsUsingTwoHandedSword", true);
        }
        else if (weapon.name.StartsWith("Wand"))
        {
            animator.SetBool("IsUsingMagicWand", true);
        }
        // ... add other weapon specific checks here ...
    }
}
