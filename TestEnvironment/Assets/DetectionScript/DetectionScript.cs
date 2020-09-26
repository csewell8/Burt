using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DetectionScript : MonoBehaviour
{
    struct FruitConsumable
    {
        public GameObject go_Fruit;
        public Vector3 v_Fruit;
        public FruitConsumable(GameObject o, Vector3 v)
        {
            this.go_Fruit = o;
            this.v_Fruit = v;
        }
    };
    
    private FruitConsumable closestFruit;
    private FruitConsumable farFruit = new FruitConsumable(null, new Vector3((float)1000000.0, (float)1000000.0, (float)1000000.0) );
    public GameObject Bird;

// Start is called before the first frame update
void Start()
    {
        closestFruit = farFruit;
    }



    // Update is called once per frame
    //   void Update()
    //   {
    //       
    //   }
 
    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log(closestFruit.go_Fruit.name);

        if (collision.gameObject.CompareTag("Fruit"))
        {
            if(Vector3.Distance(collision.gameObject.transform.position, Bird.transform.position) < Vector3.Distance(closestFruit.v_Fruit, Bird.transform.position))
            {
                FruitConsumable newFruit;
                newFruit.v_Fruit = collision.gameObject.transform.position;
                newFruit.go_Fruit = collision.gameObject;
                closestFruit = newFruit;
                Debug.Log(closestFruit.go_Fruit.name);
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.name == closestFruit.go_Fruit.name)
        {
            closestFruit = farFruit;
        }
    }
}
