using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{

    public GameObject[] waypoints;
    public GameObject player;
    int current = 0;
    public float speed;
    float WPradius = 1;
    private CharacterController controller;
    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    } 

    
    void Update () 
    {
        //print( transform.position);
        //print(waypoints[current].transform.position);
        if(Vector3.Distance(waypoints[current].transform.position, player.transform.position) < WPradius)
        {
            current = Random.Range(0,waypoints.Length);
            if (current >= waypoints.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(player.transform.position, waypoints[current].transform.position, Time.deltaTime * speed);

    }

    void OnTriggerEnter(Collider n)
    {
        if (n.gameObject == player)
        {
            player.transform.parent = transform;
        }
    }
    void OnTriggerExit(Collider n)
    {
        if (n.gameObject == player)
        {
            player.transform.parent = null;
        }
    }

}
