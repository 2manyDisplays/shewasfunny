using UnityEngine;


public class FreeFlightController : MonoBehaviour {
    [Tooltip("Enable/disable rotation control. For use in Unity editor only.")]
    public bool rotationEnabled = true;

    [Tooltip("Enable/disable translation control. For use in Unity editor only.")]
    public bool translationEnabled = true;

    [Tooltip("Mouse sensitivity")]
    public float mouseSensitivity = 2f;

    [Tooltip("Straffe Speed")]
    public float straffeSpeed = 5f;

    [Tooltip("Boost Speed")]
    public float boostSpeedMultiplyer = 3f;

    private float boostSpeed = 1;

    private float minimumX = -360f;
    private float maximumX = 360f;

    private float minimumY = -90f;
    private float maximumY = 90f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.localRotation;
    }

    void Update() {
        
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * straffeSpeed * boostSpeed;
            float z = Input.GetAxis("Vertical") * Time.deltaTime * straffeSpeed * boostSpeed;

           

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            boostSpeed = boostSpeedMultiplyer;  
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            boostSpeed = 1;
        }

        transform.Translate(x, 0, z);

        if (rotationEnabled && Input.GetMouseButton(0))
        {

            rotationX += Input.GetAxis ("Mouse X") * mouseSensitivity;
            rotationY += Input.GetAxis ("Mouse Y") * mouseSensitivity;

            rotationX = ClampAngle (rotationX, minimumX, maximumX);
            rotationY = ClampAngle (rotationY, minimumY, maximumY);

            Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, Vector3.left);

            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
    }

    public static float ClampAngle (float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;
        return Mathf.Clamp (angle, min, max);
    }
}
