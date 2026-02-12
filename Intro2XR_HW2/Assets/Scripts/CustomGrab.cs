using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    // This script should be attached to both controller objects in the scene
    // Make sure to define the input in the editor (LeftHand/Grip and RightHand/Grip recommended respectively)
    CustomGrab otherHand = null;
    public List<Transform> nearObjects = new List<Transform>();
    public Transform grabbedObject = null;
    public InputActionReference action;
    public InputActionReference doubleRotationAction;
    bool grabbing = false;
    bool doubleRotation = false;
    Vector3 lastPosition;
    Vector3 hand2object;
    Vector3 rotatedPosition;
    Quaternion lastRotation;
    Quaternion deltaRotation;


    private void Start()
    {
        action.action.Enable();
        doubleRotationAction.action.Enable();
        doubleRotationAction.action.performed += (ctx) =>
        {
            doubleRotation = !doubleRotation;
        };

        // Find the other hand
        foreach (CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
        {
            if (c != this)
                otherHand = c;
        }
    }

    void Update()
    {
        grabbing = action.action.IsPressed();
        if (grabbing)
        {
            // Grab nearby object or the object in the other hand
            if (!grabbedObject)
            {
                grabbedObject = nearObjects.Count > 0 ? nearObjects[0] : otherHand.grabbedObject;
                if (grabbedObject)
                {
                    grabbedObject.GetComponent<Rigidbody>().useGravity = false;
                    grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                }
            }

            if (grabbedObject)
            {

                // Change these to add the delta position and rotation instead
                // Save the position and rotation at the end of Update function, so you can compare previous pos/rot to current here
                grabbedObject.transform.position += transform.position - lastPosition;

                // get the vector from the translated controller to the translated object
                hand2object = grabbedObject.transform.position - transform.position;
                // get the delta rotation
                // see if these are the right way around...
                deltaRotation = Quaternion.Inverse(lastRotation) * transform.rotation;
                // and rotate the vector by the quaternion
                rotatedPosition = deltaRotation * hand2object;
                // "rotate" the object to the correct position
                grabbedObject.transform.position += rotatedPosition - hand2object;
                // do this here so there's only 1 if
                if (doubleRotation)
                {
                    grabbedObject.transform.position += rotatedPosition - hand2object;
                    grabbedObject.transform.rotation = deltaRotation * grabbedObject.transform.rotation;
                }
                // and then rotate the object to the correct orientation
                grabbedObject.transform.rotation = deltaRotation * grabbedObject.transform.rotation;

            }
        }
        // If let go of button, release object
        else if (grabbedObject)
        {
            grabbedObject.GetComponent<Rigidbody>().useGravity = true;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject = null;
        }
        // Should save the current position and rotation here
        lastPosition = transform.position;
        lastRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Make sure to tag grabbable objects with the "grabbable" tag
        // You also need to make sure to have colliders for the grabbable objects and the controllers
        // Make sure to set the controller colliders as triggers or they will get misplaced
        // You also need to add Rigidbody to the controllers for these functions to be triggered
        // Make sure gravity is disabled though, or your controllers will (virtually) fall to the ground

        Transform t = other.transform;
        if(t && t.tag.ToLower()=="grabbable")
            nearObjects.Add(t);
    }

    private void OnTriggerExit(Collider other)
    {
        Transform t = other.transform;
        if( t && t.tag.ToLower()=="grabbable")
            nearObjects.Remove(t);
    }
}
