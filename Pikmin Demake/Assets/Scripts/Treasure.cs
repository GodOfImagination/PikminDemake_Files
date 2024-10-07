using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public Vector3 Rotation;        // Which direction the treasure rotates.
    public float RotationSpeed;     // How fast the treasure rotates.
    public bool CanRotate = true;

    void Update()
    {
        if (CanRotate == true)
            transform.Rotate(Rotation * RotationSpeed * Time.deltaTime);


    }
}
