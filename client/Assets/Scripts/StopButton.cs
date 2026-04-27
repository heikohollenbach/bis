using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Attach directly to a UI Image.
/// No Button component needed — uses IPointerClickHandler instead,
/// so it is compatible with GameObjects that already have a Slider.
/// When clicked, sets the ship speed to 0 and resets the ThrustSlider.
/// </summary>
public class StopButton : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("The ship controller to stop.")]
    public CubeMovement shipController;

    [Tooltip("Optional: the thrust slider to reset to 0.")]
    public SpeedSlider speedSlider;

    private void Start()
    {
        if (shipController is null)
            Debug.LogWarning($"[{nameof(StopButton)}] No ship controller assigned.");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        shipController?.SetSpeed(0f);

        if (speedSlider is not null)
            speedSlider.GetComponent<Slider>().value = 0f;
    }
}
