using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attaches to a UI Slider and forwards the slider value to <see cref="CubeMovement.SetSpeed"/>.
/// Configure the Slider in the inspector:
///   Direction      = Bottom To Top
///   Min Value      = -20
///   Max Value      =  20
///   Whole Numbers  = false
///   Value          =  0
/// Anchor the Slider to the bottom-left corner of the Canvas.
/// </summary>
[RequireComponent(typeof(Slider))]
public class SpeedSlider : MonoBehaviour
{
    [Tooltip("The ship controller to drive.")]
    public CubeMovement shipController;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.direction = Slider.Direction.BottomToTop;
        _slider.minValue = -20f;
        _slider.maxValue = 20f;
        _slider.wholeNumbers = false;
    }

    private void Start()
    {
        _slider.value = 0f;
        _slider.onValueChanged.AddListener(OnSpeedChanged);

        if (shipController is null)
            Debug.LogWarning($"[{nameof(SpeedSlider)}] No ship controller assigned. Drag a GameObject with CubeMovement into the Inspector.");
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(OnSpeedChanged);
    }

    private void OnSpeedChanged(float value)
    {
        shipController?.SetSpeed(value);
    }
}
