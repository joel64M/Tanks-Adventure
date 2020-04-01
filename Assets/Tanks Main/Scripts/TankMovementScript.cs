using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovementScript : MonoBehaviour
{
    #region Variables
    public VariableJoystick joystick;
    public float forceMultiplier=10f;
    public float maxSpeed = 10f;


    Vector3 input;
    Rigidbody rb;

    [Header("Movement Properties")]
    public float moveSpeed = 10f;
    public float turnSpeed = 180f;
    float movementInputValue;
    float turnInputValue;


    [Header("Audio Properties")]
    public AudioSource movementAudioSource;
    public AudioClip engineIdle;
    public AudioClip engineRunning;
    float originalPitch;
    public float pitchRange = 0.2f;
  

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPitch = movementAudioSource.pitch;
 

    }

    // Update is called once per frame
    private void Update()
    {
        movementInputValue = Input.GetAxis("Vertical");
        turnInputValue = Input.GetAxis("Horizontal");
        EngineAudio();
    }

    void FixedUpdate()
    {
        Move();
        Turn();
    }
    void Move()
    {
        Vector3 movement = transform.forward * movementInputValue * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }
    void Turn()
    {
        float turn = turnInputValue * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(transform.rotation * turnRotation);
    }
    void EngineAudio()
    {
        if(Mathf.Abs(movementInputValue)<0.1f && Mathf.Abs(turnInputValue) < 0.1f)
        {
            if(movementAudioSource.clip == engineRunning)
            {
                movementAudioSource.clip = engineIdle;
                movementAudioSource.pitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
                movementAudioSource.Play();
            }
        }
        else
        {
            if (movementAudioSource.clip == engineIdle)
            {
                movementAudioSource.clip = engineRunning;
                movementAudioSource.pitch = Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
                movementAudioSource.Play();
            }
        }
    }

    void MoveViaForce()
    {
        // Vector2 ip = new Vector2(Joystick)
        input = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        transform.LookAt(transform.position + input);
        rb.AddForce(input * forceMultiplier);
        if (!joystick.isActiveAndEnabled)
        {
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
        }
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }
}
