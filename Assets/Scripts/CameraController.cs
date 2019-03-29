using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;    
    
    public bool useOffsetValues;

    public float rotateSpeed;

    public Transform pivot;

    public float maxViewAngle;
    public float minViewAngle;

    public bool invertY;

    
    void Start()
    {
        if(!useOffsetValues)
        {
            offset = target.position-transform.position;
        }

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        Cursor.lockState = CursorLockMode.Locked;
    }

    //update iss called once per frame
    void LateUpdate()
    {
        //get the x position of the mouse & rotate the target
        float horizontal =  Input.GetAxisRaw("Mouse X") * rotateSpeed;
        target.Rotate(0, horizontal, 0);

        //get the y position of the mouse and rotate the pivot
        float vertical =  Input.GetAxisRaw("Mouse Y") * rotateSpeed;
        
        if(invertY)
        {
            pivot.Rotate(vertical, 0, 0);
        }
        else
        {
            pivot.Rotate(-vertical, 0, 0);
        }

        //limit up/down camera rotation
        if (pivot.rotation.eulerAngles.x>maxViewAngle && pivot.rotation.eulerAngles.x<180)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle,0,0);
        }
        if(pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x <360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }
        
        //move the camera based on the current rotation of the target and the original offset
        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

        //transform.position=target.position-offset;        

        if (transform.position.y<target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y-0.7f, transform.position.z);
            //Mathf.Lerp(1f, 5f, 0.5f);
        }
    
        transform.LookAt(target);
    }

}