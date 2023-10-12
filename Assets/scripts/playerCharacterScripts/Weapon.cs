using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    public bool isPickedUp = false;
    public string weaponName;
    public float damage;
    public AnimationClip attackAnimation;

    public abstract void Use();
}
