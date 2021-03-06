using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;      // how far the camera should be from player

    public bool useOffsetValues;

    public float rotateSpeed;   // how fast camera rotates around player;

    public Transform pivot;

    public float maxViewAngle;
    public float minViewAngle;

    public bool invertY;        // keep it always unchecked

    // Start is called before the first frame update
    void Start()
    {
        if (!useOffsetValues) {
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        // hide cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // get the x position of the mouse and rotate the target;
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0, horizontal, 0);

        // get the y position of the mouse and rotate the pivot
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        // pivot.Rotate(-vertical, 0, 0);                      
        if (invertY)
        {
            pivot.Rotate(vertical, 0, 0);
        }
        else {
            pivot.Rotate(-vertical, 0, 0);
        }

        // Limit the up/down camera rotation
        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f) {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }

        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }

        // Move the camera on the base of current rotation of target and the original offset
        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

        if (transform.position.y < target.position.y) {
            transform.position = new Vector3(transform.position.x, target.position.y - 0.5f, transform.position.z);
        }

        transform.LookAt(target);    
    }
}
