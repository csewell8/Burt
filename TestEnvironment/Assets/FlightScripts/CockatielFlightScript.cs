using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CockatielFlightScript : MonoBehaviour
{
    public GameObject Cockatiel;
    public GameObject GroundMovement;
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
    private const int dead = 0b0000;
    private int walkingSpeed = 8;
    private int velCap = 20;

    public int movementState = 0;


    // Start is called before the first frame update
    void Start()
    {
        movementState = flying;
    }

    void OnCollisionEnter(Collision collider)
    {
        if (movementState == dead) return;
        Debug.Log(collider.relativeVelocity.y);
        //Lots of different conditions will need to be established
        if (collider.gameObject.CompareTag("Ground"))
        {

            movementState = walking;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            //Something sus about what the actual values mean, RigidBody Info tab shows ~-20 for negative y when Debug says -10
            if (collider.relativeVelocity.y > 20)
            {
              //  Debug.Log("Velocity too high");
             //   Debug.Log(rb.velocity.y.ToString());
                rb.constraints = RigidbodyConstraints.None;//(rb.constraints ^ (RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationX));
                movementState = dead;
            }
           //Add a ground drag coefficient so that the bird will slow down if it comes in at highspeed and lands 
           
        }
    }
    private void OnCollisionExit(Collision collider)
    {
        if (movementState == dead) return;
        if (collider.gameObject.CompareTag("Ground"))
        {
            movementState = flying;
            rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
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

        //Apply cap on velocity
        Vector3 Velxz = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        Vector3 Vely = new Vector3(0, rb.velocity.y, 0);

        Velxz = Vector3.ClampMagnitude(Velxz, velCap);

        rb.velocity = Velxz + Vely;

        if (movementState == dead) return;

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
        if (movementState == flying)
        {
            if (Input.GetKey(KeyCode.W))
            {

                rb.AddForce(transform.forward * forwardMovement * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(-(transform.forward * forwardMovement * Time.deltaTime));
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                Vector3 forwardVel = (rb.transform.forward * walkingSpeed) +  new Vector3(0,rb.velocity.y,0);

                rb.velocity = (forwardVel);// * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Vector3 backwardVel = -(rb.transform.forward * walkingSpeed) + new Vector3(0, rb.velocity.y, 0);
                rb.velocity = (backwardVel);// *  Time.deltaTime);
            }
            else
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
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
