using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil : MonoBehaviour
{
    public ThirdPersonPlayer thirdPersonPlayer;
    public PlayerInventory playerInventory;
    public BoxCollider weaponCollider;
    public GameObject currWeapon;

    public void Start(){
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        currWeapon = playerInventory.currentMeleeWeapon;
        weaponCollider = currWeapon.GetComponent<BoxCollider>();
    }

    public void Use()
    {  

        currWeapon.GetComponent<WeaponStats>().atkDmg = (int)(currWeapon.GetComponent<WeaponStats>().atkDmg * 1.5);//changes the weapons damage to be 1.5 times as much 
        weaponCollider.size = new Vector3((float)(weaponCollider.size.x * 1.5), (float)(weaponCollider.size.y * 1.5), (float)(weaponCollider.size.z * 1.5));
        currWeapon.GetComponent<WeaponStats>().upgradeNums += 1;
        // Get current weapon the player is holding
        // Check if it is already upgraded
        //      Let's say every weapon starts at tier 0
        //      Upgrade to tier 1 is like 200 points
        //      Then tier 2 is maybe 500?
        //      Tier 3 is 1000? something crazy (this is max level)
        //      (Cost is currently handled in the ThirdPersonPlayer script)
        //      If you get the weapon upgrade part working I can handle the cost
        // Each tier doubles the weapon damage and increases it's size by 1.5
        // (we can tweak these numbers for balance purposes)

        Debug.Log("Used Anvil");
    }

}
