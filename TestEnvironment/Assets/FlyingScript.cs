using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FlyingScript : MonoBehaviour
{
    public GameObject UnscaledBird;
    UnityEngine.Vector2 flydirection;
    public Camera cam;
    public Rigidbody rb;
    public float thrust = 3.0f;
    public float lift = 3.0f;
    private float flapCoolDown = 0;
    public int FlapCoolDownTime = 30;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector2 centerScreenDimensions = new UnityEngine.Vector2(Screen.width/2, Screen.height/2);
        UnityEngine.Vector2 mouseXY = new UnityEngine.Vector2(Input.mousePosition.x, Input.mousePosition.y);
        flydirection = centerScreenDimensions - mouseXY;
        flapCoolDown += Time.deltaTime;

        int y = (int) flydirection.y;
        if (flydirection.y > (Screen.height/6))
         {
            transform.Rotate(new UnityEngine.Vector3(1,0,0)*Time.deltaTime * 15.0f * (((int)flydirection.y ^ 2) / (Screen.height / 10)));
         }
        if (flydirection.y < -(Screen.height / 6))
        {
            transform.Rotate(new UnityEngine.Vector3(-1, 0, 0) * Time.deltaTime * 15.0f * -(((int)flydirection.y ^ 2) / (Screen.height / 10)));
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (flapCoolDown > (Time.deltaTime * FlapCoolDownTime))
            {
                rb.AddForce(transform.forward * thrust);
                rb.AddForce(transform.up * lift);
                flapCoolDown = 0;
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
                rb.AddForce(transform.forward * thrust * 3.0f * Time.deltaTime);        
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = rb.velocity * 0.99f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //rb.AddRelativeTorque(transform. * Time.deltaTime);
            float turn = Input.GetAxis("Horizontal");
            rb.AddTorque(transform.forward * -0.1f * turn * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //rb.AddRelativeTorque(transform. * Time.deltaTime);
            float turn = Input.GetAxis("Horizontal");
            rb.AddTorque(transform.forward * -0.1f * turn * Time.deltaTime);
        }


    }
}