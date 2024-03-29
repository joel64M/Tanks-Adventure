﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NameSpaceName {

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(TankInput))]
    public class TankMotor : MonoBehaviour
    {

        #region Variables
        [Header("Control Choice")]
        [SerializeField] bool moveByForce = true;
        [SerializeField] bool isAudio = true;

        [Header("Motor Properties")]
        public float moveSpeed = 5f;
        public float forceMultiplier = 50f;
        public float maxSpeed = 10f;
        public float turnSpeed = 10f;

        [Header("Audio Properties")]
        public AudioSource movementAudioSource;
        public AudioClip engineIdle;
        public AudioClip engineRunning;
        public float addPitch = 0.2f;
        float originalPitch;

        //components
        TankInput tankInputScript;
        Rigidbody rb;
        bool isNavMesh;
        #endregion

        #region Builtin Methods


        void Awake()
        {
            tankInputScript = GetComponent<TankInput>();
         
            rb = GetComponent<Rigidbody>();
            originalPitch = movementAudioSource.pitch;
            
        }

        void FixedUpdate()
        {
            if (!isNavMesh)
            {
                if (rb)
                {
                    if (moveByForce)
                    {
                        MoveByForce();
                    }
                    else
                    {
                        Move();
                        Turn();
                    }
                }
            }
            if(isAudio)
            if (movementAudioSource)
            {

                EngineAudio();
            }
        }
  
        #endregion

        #region Custom Methods

        public void SetAiTankMotor()
        {
            isNavMesh = true;
        }
        void Move()
        {
            Vector3 movement = transform.forward * tankInputScript.VerticalInputValue * moveSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + movement);
        }

        void Turn()
        {
            float turn = tankInputScript.HorizontalInputValue * turnSpeed * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(transform.rotation * turnRotation);
        }

        void MoveByForce()
        {
            // Vector2 ip = new Vector2(Joystick)
            Vector3 input = new Vector3(tankInputScript.HorizontalInputValue, 0, tankInputScript.VerticalInputValue);
            transform.LookAt(transform.position + input);

            rb.AddForce(input * forceMultiplier);
          
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

        void EngineAudio()
        {
            if (Mathf.Abs(tankInputScript.HorizontalInputValue) < 0.1f && Mathf.Abs(tankInputScript.VerticalInputValue) < 0.1f)
            {
                if (movementAudioSource.clip == engineRunning)
                {
                    movementAudioSource.clip = engineIdle;
                    movementAudioSource.pitch = originalPitch;  // Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
                                                                //   movementAudioSource.Play();
                    movementAudioSource.volume = 0;
                }
            }
            else
            {
                if (movementAudioSource.clip == engineIdle)
                {
                    movementAudioSource.clip = engineRunning;
                    movementAudioSource.pitch = originalPitch + addPitch; //Random.Range(originalPitch - pitchRange, originalPitch + pitchRange);
                    movementAudioSource.volume = 0.1f;
                    movementAudioSource.Play();
                }
            }
        }

        #endregion

    }
}
