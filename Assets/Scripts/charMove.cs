using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class charMove : MonoBehaviour
{
    [Space(5)]
    [Header("Character Movement Controller")]
    [Space(5)]
    [Range(5f, 60f)]
    public float slopeLimit = 45f;
    public float moveSpeed = 2f;
    public float turnSpeed = 300f;
    public bool allowJump = false;
    public float jumpSpeed = 4f;
    public bool isGrounded;
    public float forwardInput;
    public float turnInput;
    public bool jumpInput;
    new private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;

    [Space(5)]
    [Header("Audio Controller")]
    [Space(5)]
    public AudioSource jumpSound;
    public AudioSource collectSound;

    [Space(5)]
    [Header("Blend Shape")]
    [Space(5)]
    [SerializeField] bool isCompletedMaxHeight;
    [SerializeField] bool shape;
    [Range(0, 100)]
    [SerializeField] float shapeWeight;
    [SerializeField] float shapeIncreaseSpeed = 85f;
    SkinnedMeshRenderer skin;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        skin = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();

        shapeWeight = skin.GetBlendShapeWeight(0);
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        ProcessActions();
    }

    void Update()
    {
        CheckShape();
    }

    private void CheckShape()
    {
        skin.SetBlendShapeWeight(0, shapeWeight);
        if (shape)
        {
            if (shapeWeight < 100f && !isCompletedMaxHeight)
            {
                shapeWeight += shapeIncreaseSpeed * Time.deltaTime;
            }
            else
            {
                isCompletedMaxHeight = true;
                shapeWeight -= shapeIncreaseSpeed * Time.deltaTime;
                if (shapeWeight <= 0)
                {
                    shape = false;
                    isCompletedMaxHeight = false;
                }
            }
        }
    }


    private void CheckGrounded()
    {
        isGrounded = false;
        float capsuleHeight = Mathf.Max(capsuleCollider.radius * 2f, capsuleCollider.height);
        Vector3 capsuleBottom = transform.TransformPoint(capsuleCollider.center - Vector3.up * capsuleHeight / 2f);
        float radius = transform.TransformVector(capsuleCollider.radius, 0f, 0f).magnitude;

        Ray ray = new Ray(capsuleBottom + transform.up * .01f, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, radius * 5f))
        {
            float normalAngle = Vector3.Angle(hit.normal, transform.up);

            if (normalAngle < slopeLimit)
            {
                float maxDist = radius / Mathf.Cos(Mathf.Deg2Rad * normalAngle) - radius + .02f;

                if (hit.distance < maxDist)
                {
                    isGrounded = true;
                }
            }
        }
    }

    private void ProcessActions()
    {
        if (turnInput != 0f)
        {
            float angle = Mathf.Clamp(turnInput, -1f, 1f) * turnSpeed;
            transform.Rotate(Vector3.up, Time.fixedDeltaTime * angle);
        }

        Vector3 move = transform.forward * Mathf.Clamp(forwardInput, -1f, 1f) * moveSpeed * Time.fixedDeltaTime;
        rigidbody.MovePosition(transform.position + move);

        if (jumpInput && allowJump && isGrounded)
        {
            rigidbody.AddForce(transform.up * jumpSpeed, ForceMode.VelocityChange);
        }
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
            GameManager.instance.OpenPanel();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Grounded") && !isGrounded)
        {
            shape = true;
        }
    }

}