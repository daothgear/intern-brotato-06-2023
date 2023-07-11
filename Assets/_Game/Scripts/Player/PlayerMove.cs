using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerMove : MonoBehaviour
{
    public Joystick joystick;
    public float speed = 5f;
    
    private void Update()
    {
        transform.position += new Vector3(joystick.Horizontal, joystick.Vertical, 0) * Time.deltaTime * speed;
    }
}
