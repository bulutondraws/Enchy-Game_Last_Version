using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveObject : MonoBehaviour
{
    private charMove cm;
    //public GameObject coloredobject;
    public float pushforce = 0f;
    private CharacterController controller;
    
    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    } 
    
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enchy")
        {
            //pushforce = cm.playerSpeed;
            Debug.Log("hit");
            Transform box = this.GetComponent<Transform>();
            float distance = Vector3.Distance(box.position, transform.position);
      

            if (distance < 0.005f)
            {
                
                //coloredobject = GameObject.FindWithTag("Enchy");
                box.GetComponent<Rigidbody>().isKinematic = true;
                //coloredobject.GetComponent<Renderer>().material.color = box.GetComponent<Renderer>().material.color;
                //Destroy(box.gameObject, 2f);
                Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                controller.Move(move * Time.deltaTime * pushforce);

            }
        }
    }
}
