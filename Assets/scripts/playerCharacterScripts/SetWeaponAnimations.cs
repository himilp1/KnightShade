using UnityEngine;

public class SetWeaponAnimations : MonoBehaviour{
    [SerializeField] private AnimatorOverrideController[] overrideControllers;
    [SerializeField] private AnimatorOverrider overrider;

    public void Set(int value){
        overrider.SetAnimations(overrideControllers[value]);
    }
}