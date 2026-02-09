using UnityEngine;
using UnityEngine.InputSystem;

public class PlanetRotator : MonoBehaviour
{
    public InputActionReference action;
    bool rotating = false;
    public float degreesPerSecond = 10.0f;

    void Start()
    {
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            rotating = !rotating;
        };
    }

    void Update()
    {
        if (rotating)
        {
            transform.Rotate(0, degreesPerSecond * Time.deltaTime, 0);
        }
    }
}
