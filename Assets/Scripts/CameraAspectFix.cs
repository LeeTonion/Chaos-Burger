using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAspectFix : MonoBehaviour
{
    public float referenceVerticalFOV = 60f; 

    void Start()
    {
        Camera cam = GetComponent<Camera>();

        float referenceAspect = 9f / 16f;
        float currentAspect = (float)Screen.width / Screen.height;

        float verticalFOVRad = referenceVerticalFOV * Mathf.Deg2Rad;
        float horizontalFOVRad = 2f * Mathf.Atan(Mathf.Tan(verticalFOVRad / 2f) * currentAspect / referenceAspect);

        cam.fieldOfView = horizontalFOVRad * Mathf.Rad2Deg;
    }
}
