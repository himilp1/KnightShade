using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollPotion : MonoBehaviour
{
    public ThirdPersonPlayer thirdPersonPlayer;
    public ThirdPersonRoll thirdPersonRoll;
    public RollCooldown rollCooldown;

    private CanvasGroup canvasGroup;

    public AudioSource potionSound;

    // Start is called before the first frame update
    void Start()
    {
        thirdPersonRoll = thirdPersonPlayer.GetComponent<ThirdPersonRoll>();
        canvasGroup = rollCooldown.GetComponent<CanvasGroup>();
        thirdPersonRoll.enabled = false;
        canvasGroup.alpha = 0;
    }

    public void Consume()
    {
        potionSound.Play();
        thirdPersonRoll.enabled = true;
        canvasGroup.alpha = 1;
        Debug.Log("Consumed Roll Potion");
        gameObject.SetActive(false);
    }
}