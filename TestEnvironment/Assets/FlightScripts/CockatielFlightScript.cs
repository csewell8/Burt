using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CockatielFlightScript : MonoBehaviour
{
    public GameObject Cockatiel;
    public float lift = 2.0f;
    public float flapCoolDown = 0;
    public float flapCoolDownTime = 30;
    public Camera cam;
    public Rigidbody rb;
    public float forwardMovement = 2.0f;
    public float rotationSpeed = 3.0f;
    public Vector3 forward;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        forward = transform.forward;
        flapCoolDown += Time.deltaTime;
        if(transform.rotation.x != 0)
        {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (flapCoolDown > (Time.deltaTime * flapCoolDownTime))
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(transform.up * lift);
                flapCoolDown = 0;
            }
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.right * forwardMovement * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-(transform.right * forwardMovement * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new UnityEngine.Vector3(0, -1, 0) * rotationSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new UnityEngine.Vector3(0, 1, 0) * rotationSpeed);
        }
    }
}
