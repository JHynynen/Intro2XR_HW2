using UnityEngine;

public class LensCameraScript : MonoBehaviour
{

    public Camera lensCamera;
    public GameObject lens;
    private float distance;

    // Update is called once per frame
    void Update()
    {
        lensCamera.transform.LookAt(lens.transform);

        distance = Vector3.Distance(lens.transform.position, lensCamera.transform.position);
        lensCamera.nearClipPlane = distance + 0.1f;
    }
}
