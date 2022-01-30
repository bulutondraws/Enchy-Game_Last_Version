using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class charMove : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    [SerializeField] bool groundedPlayer;
    [SerializeField] bool shape;
    [SerializeField] float shapeWeight;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public AudioSource jumpSound;
    public AudioSource collectSound;
    SkinnedMeshRenderer skin;
    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        skin = GetComponent<SkinnedMeshRenderer>();
        
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

            if (!jumpSound.isPlaying)
            {
                jumpSound.Play();
            }
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //if (groundedPlayer && shape)
        //{
        //    shapeWeight += 50f;
        //    skin.SetBlendShapeWeight(0, shapeWeight);
        //    if (shapeWeight >= 100)
        //    {
        //        shapeWeight -= 50f;
        //        skin.SetBlendShapeWeight(0, shapeWeight);
        //        if (shapeWeight <= 0)
        //        {
        //            shape = false;
        //            shapeWeight = 0;
        //        }
        //    }
        //}
    }

    public void BoostPlaySound()
    {
        collectSound.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Booster"))
        {
            GameManager.instance.BoosterParticleInvoke(other.transform.position);
        }
        else if (other.transform.CompareTag("Finish"))
        {
            gameObject.SetActive(false);
            ButtonManager.instance.LevelCompleted();
        }
        else if (other.transform.CompareTag("GameOver"))
        {
            gameObject.SetActive(false);
            ButtonManager.instance.GameOver();
        }
    }

}