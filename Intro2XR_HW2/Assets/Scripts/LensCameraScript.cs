using UnityEngine;

public class LensCameraScript : MonoBehaviour
{

    public Camera lensCamera;
    public GameObject lens;
    public Camera playerHead;
    public GameObject dummy;
    private float distance;
    Quaternion fullTurn = new Quaternion(0, 1, 0, 0);
    Quaternion diff;
    Quaternion diffY;

    void Update()
    {
        /*
        lensCamera.transform.LookAt(playerHead.transform);
        lensCamera.transform.rotation = fullTurn * lensCamera.transform.rotation;
        */


        
        dummy.transform.LookAt(lens.transform);
        diff = dummy.transform.rotation * Quaternion.Inverse(lens.transform.rotation);
        diffY = new Quaternion(0, 0, diff[2], diff[3]);
        diffY = Quaternion.Normalize(diffY);
        lensCamera.transform.rotation = lens.transform.rotation;
        




        /*
        lensCamera.transform.rotation = lens.transform.rotation;
        lensCamera.transform.LookAt(lens.transform);

        distance = Vector3.Distance(lens.transform.position, lensCamera.transform.position);
        lensCamera.nearClipPlane = distance + 0.1f;
        */
    }
}
