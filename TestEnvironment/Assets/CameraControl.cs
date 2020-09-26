using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public Camera cam;
    public GameObject player;
    private float rotationAngle = 100f;
    public Vector3 CameraOffset;
    private float cameraRotationNormalizer = 90.0f;
    private float birdAngle;
    // Start is called before the first frame update
    void Start()
    {
        rotationAngle = 100f;
       // CameraOffset = new Vector3(player.transform.position.x - 0.38f, player.transform.position.y + 3.03f, player.transform.position.z + 7.8f);
    }

    private void LateUpdate()
    {
       // CameraOffset = new Vector3(player.transform.position.x - 0.38f, player.transform.position.y + 3.03f, player.transform.position.z + 7.8f);
    }
    // Update is called once per frame
    void Update()
    {
        birdAngle = player.transform.rotation.eulerAngles.y;

        float normalizedCam = ((cam.transform.rotation.eulerAngles.y - cameraRotationNormalizer)) % 360;
        if(normalizedCam > 180.0f)
        {
            normalizedCam = normalizedCam - 360f;
        }

        float modNum = (-10 ) % 360;
        Debug.Log(modNum.ToString());
        if (Input.GetKey(KeyCode.Q))
       {
           //Rotate camera around the bird clockwise
           cam.transform.RotateAround(player.transform.position, new Vector3(0,1,0), (float)(rotationAngle * Time.deltaTime));
            Debug.Log(normalizedCam.ToString());
        }
      else if(Input.GetKey(KeyCode.E))
      {
            //Rotate camera around the bird counterclockwise
            cam.transform.RotateAround(player.transform.position, new Vector3(0, 1, 0), -(float)(rotationAngle * Time.deltaTime));
            Debug.Log(normalizedCam.ToString());
        }
      else
      {
            if(normalizedCam > 0.5f || normalizedCam < -0.5f)
            {
                float diff = normalizedCam;
                cam.transform.RotateAround(player.transform.position, new Vector3(0, 1, 0), -(float)(normalizedCam/5));
            }
            //Nothing pressed so just rotate back to behind the bird
            else
            {
                cam.transform.RotateAround(player.transform.position, new Vector3(0, 1, 0), -(float)(normalizedCam));
            }
      }
    
    }
}
