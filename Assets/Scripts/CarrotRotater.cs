using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotRotater : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up * 3f, Space.World);
    }
}
