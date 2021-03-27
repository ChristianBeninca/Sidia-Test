using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public int speed;

    void Update()
    {
        transform.Rotate(0,speed * Time.deltaTime,0);
    }
}
