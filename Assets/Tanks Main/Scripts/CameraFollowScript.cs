﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{

    #region Variables
    [Header("Camera Properties")]
    public Transform target;
    public float smoothTime = 0.5f;
    public bool lookAtTarget = true;
	public float clampX = 10f;
	public float clampZ = 15f;
	Vector3 offset, wantedPosition;

    private Vector3 currentVelocity;

    #endregion


    #region Builtin Methods
    // Start is called before the first frame update
    void Start()
    {
        CalculateOffset();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleCamera();
    }

    #endregion

    #region Custom Methods
    void CalculateOffset()
    {
        offset = target.position + transform.position;
    }

    protected virtual void HandleCamera()
    {
        if (target)
        {
            wantedPosition = target.position + offset;
          //  wantedPosition.x = transform.position.x;
          
            Vector3 tempPos= Vector3.SmoothDamp(transform.position, wantedPosition, ref currentVelocity, smoothTime);
            tempPos.x = Mathf.Clamp(tempPos.x, -clampX, clampX);
            tempPos.z = Mathf.Clamp(tempPos.z, -clampZ, clampZ);

            transform.position = tempPos;
            if (lookAtTarget)
            {
                transform.LookAt(target);
            }
        }
    }
    #endregion

}
