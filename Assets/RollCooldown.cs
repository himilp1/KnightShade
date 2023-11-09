using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RollCooldown : MonoBehaviour
{
    public UnityEngine.UI.Slider slider;
    public UnityEngine.UI.Image fill;
    public float duration = 2f; // Duration in seconds for the animation

    public void StartCooldownAnimation()
    {
        StartCoroutine(IncreaseSliderValueOverTime());
    }

    private IEnumerator IncreaseSliderValueOverTime()
    {
        float elapsedTime = 0f;
        float startValue = 0f;
        float endValue = slider.maxValue;
        fill.color = Color.red;

        while (elapsedTime < duration)
        {
            // Incrementally increase the slider value
            slider.value = Mathf.Lerp(startValue, endValue, elapsedTime / duration);

            // Update the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        // Ensure the slider reaches its max value at the end
        slider.value = endValue;
        fill.color = Color.green;
    }
}
