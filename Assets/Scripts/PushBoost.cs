using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBoost : MonoBehaviour
{
    //public Vector3 jumpCoef;
    public MoveObject mo;
    public float PushCoef;

    
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enchy")
        {
              
           
            mo.pushforce = PushCoef;


            
            Debug.Log("You currently have pushing power.");
            
            other.transform.GetChild(0).GetComponent<Renderer>().material.color = gameObject.GetComponent<Renderer>().material.color;


            Destroy(gameObject);
        }
    }
}
