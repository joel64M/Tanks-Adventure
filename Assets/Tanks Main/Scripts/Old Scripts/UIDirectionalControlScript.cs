﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace tankTutorial { 
public class UIDirectionalControlScript : MonoBehaviour
{

    public bool useRelativeRotation = true;

    Quaternion relativeRotation;

    // Start is called before the first frame update
    void Start()
    {
        relativeRotation = transform.parent.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (useRelativeRotation)
        {
            transform.rotation = relativeRotation;
        }
    }
}
}