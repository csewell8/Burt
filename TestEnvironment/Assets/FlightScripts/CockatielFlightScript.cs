using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CockatielFlightScript : MonoBehaviour
{
    public GameObject Cockatiel;
    //public Collision birdfeet;
    public float lift = 2.0f;
    public float flapCoolDown = 0;
    public float flapCoolDownTime = 30;
    public Camera cam;
    public Rigidbody rb;
    public float forwardMovement = 0.75f;
    public float rotationSpeed = 3.0f;
    public Vector3 forward;
    private const int flying = 0b0001;
    private const int walking = 0b0010;
    
    public int movementState = 0;


    // Start is called before the first frame update
    void Start()
    {
        movementState = flying;
    }

    void OnCollisionEnter(Collision collider)
    {
     //   Debug.Log(collider.gameObject.tag.ToString());
        //Lots of different conditions will need to be established
     //   if (collider.gameObject.CompareTag("Ground"))
        {

            movementState = walking;
            //Something sus about what the actual values mean, RigidBody Info tab shows ~-20 for negative y when Debug says -10
            if (rb.velocity.y > 10 || rb.velocity.y < -10)
            {
              //  Debug.Log("Velocity too high");
             //   Debug.Log(rb.velocity.y.ToString());
                rb.constraints = RigidbodyConstraints.None;//(rb.constraints ^ (RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationX));
                
            }
            
           
        }
    }
    private void OnCollisionExit(Collision collider)
    {
//        if (collider.gameObject.CompareTag("Ground"))
        {
            movementState = flying;
        }
    }
    protected void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0,  transform.localEulerAngles.y, 0);
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
