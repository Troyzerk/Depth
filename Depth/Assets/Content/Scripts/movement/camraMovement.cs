using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camraMovement : MonoBehaviour
{
    public Transform d_Player;

    private Vector3 mousePos;
    private Vector3 mouseWorld;
    private Vector3 forward;
    private Quaternion playerRot;

    public float rotationSpeed = 5f;

    // Update is called once per frame
    void Update()
    {   
        transform.position = d_Player.transform.position+new Vector3(0,6,0);
        LookAtMouse();
        
    }


    void LookAtMouse()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 characterPosition = d_Player.transform.position;
            Vector3 screenMousePosition = Camera.main.WorldToScreenPoint(characterPosition);
            Vector3 direction = mousePosition - screenMousePosition;

            if (direction != Vector3.zero)
            {
                float targetRotationAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0f, targetRotationAngle, 0f);
                d_Player.transform.rotation = Quaternion.Lerp(d_Player.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

}
