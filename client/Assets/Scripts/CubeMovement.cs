using UnityEngine;
using UnityEngine.InputSystem;

public class CubeMovement : MonoBehaviour
{
    public float speed = 10f;

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard is null)
            return;

        float input = 0f;
        if (keyboard.wKey.isPressed) input += 1f;
        if (keyboard.sKey.isPressed) input -= 1f;

        transform.Translate(Vector3.forward * input * speed * Time.deltaTime, Space.World);
    }
}
