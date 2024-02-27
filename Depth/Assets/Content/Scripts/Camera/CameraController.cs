using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Don't ask why these PanSpeed multipliers are the way they are 
    //I don't understand the math 
    //I got the 4 on the 1st try and thought I knew why
    //The 7.1 from trial and error begs to differ
    //I believe this is due to the scaling of the tilemap, both numbers are exactly half of the scale
    public float PanSpeedX = 7.125f;
    public float PanSpeedY = 4f;
    
    //Zoom control variables
    public float ZoomSpeed = 1f;
    public float MinOrthoSize = 1f;
    public float MaxOrthoSize = 4f;

    private Vector3 panOrigin;
    private Camera thisCam;

    private void Start()
    {
        thisCam = this.gameObject.GetComponent<Camera>();
        ResetCamPosition();
    }

    void Update()
    {
        Pan();
        Zoom();
    }

    public void ResetCamPosition()
    {
        thisCam.transform.position = new Vector3(PersistentManager.instance.storedPlayerTransform.x, PersistentManager.instance.storedPlayerTransform.x, thisCam.transform.position.z);
    }

    public void Pan()
    {
        // Start panning on middle mouse button down
        if (Input.GetMouseButtonDown(2))
        {
            panOrigin = Input.mousePosition;
            return;
        }

        // Return if middle mouse button is not held down
        if (!Input.GetMouseButton(2)) return;

        // Calculate new position
        Vector3 pos = Camera.main.ScreenToViewportPoint(panOrigin - Input.mousePosition);
        Vector3 move = new Vector3(pos.x * PanSpeedX, pos.y * PanSpeedY, 0);

        // Move the camera
        transform.Translate(move, Space.World);

        // Update the origin of the pan
        panOrigin = Input.mousePosition;
    }

    public void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            // Adjusting the ortho size based on the scroll input and ZoomSpeed
            // Clamp the value to make it stay within the limits
            thisCam.orthographicSize = Mathf.Clamp(thisCam.orthographicSize - scroll * ZoomSpeed, MinOrthoSize, MaxOrthoSize);
        }
    }
}

