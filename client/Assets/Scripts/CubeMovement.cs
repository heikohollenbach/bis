using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CubeMovement : MonoBehaviour
{
    /// <summary>Maximum speed in units per second.</summary>
    public float maxSpeed = 20f;

    /// <summary>How fast the speed ramps up/down when using keyboard input (units/s per second).</summary>
    public float acceleration = 10f;

    private Rigidbody _rb;

    /// <summary>Current actual speed (smoothly approaches <see cref="_targetSpeed"/>).</summary>
    private float _speed;

    /// <summary>Desired speed set by keyboard or UI. Actual speed ramps towards this value.</summary>
    private float _targetSpeed;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.isKinematic = true;
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard is not null)
        {
            if (keyboard.wKey.isPressed)
                _targetSpeed = Mathf.Clamp(_targetSpeed + acceleration * Time.deltaTime, -maxSpeed, maxSpeed);
            else if (keyboard.sKey.isPressed)
                _targetSpeed = Mathf.Clamp(_targetSpeed - acceleration * Time.deltaTime, -maxSpeed, maxSpeed);
        }

        _speed = Mathf.MoveTowards(_speed, _targetSpeed, acceleration * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (_speed == 0f)
            return;

        _rb.MovePosition(_rb.position + transform.forward * _speed * Time.fixedDeltaTime);
    }

    /// <summary>Sets the target speed from an external source (e.g. UI slider). Actual speed ramps towards it.</summary>
    /// <param name="value">Target speed value clamped to [-maxSpeed, maxSpeed].</param>
    public void SetSpeed(float value)
    {
        _targetSpeed = Mathf.Clamp(value, -maxSpeed, maxSpeed);
    }

    /// <summary>Returns the current actual speed.</summary>
    public float GetSpeed() => _speed;
}
