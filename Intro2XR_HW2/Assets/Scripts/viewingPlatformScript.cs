using UnityEngine;
using UnityEngine.InputSystem;

public class viewingPlatformScript : MonoBehaviour
{
    public InputActionReference action;
    bool onPlatform = false;
    Vector3 platformOrigin = new Vector3(60, 10, -40);
    Vector3 savedPosition = new Vector3(0, 0, 0);
    Vector3 savedRotation = new Vector3(0, 0, 0);
    

    void Start()
    {
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            if (onPlatform)
            {
                //moving back from the platform
                transform.position = savedPosition;
                transform.eulerAngles = savedRotation;
            }
            else
            {
                if (transform.position.y >= -10)
                {
                    //moving to the platform, normal conditions
                    savedPosition = transform.position;
                    savedRotation = transform.rotation.eulerAngles;
                }
                else
                {
                    //moving to the platform, fell out of the room
                    savedPosition = new Vector3(0, 0, 0);
                    savedRotation = new Vector3(0, 0, 0);
                }
                transform.position = platformOrigin;
                transform.eulerAngles = new Vector3(0, -60, 0);
            }

            onPlatform = !onPlatform;
        };
    }
}
