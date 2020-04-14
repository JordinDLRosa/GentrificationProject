using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
  private float speed = 5f;
    private void Update()
    {
        PlayerMovement();
    }
    //Movement Function
    private void PlayerMovement()
    {
      Vector3 pos = transform.position;
            //Movement Function for moving Up
            if (Input.GetKey ("w") || Input.GetKey (KeyCode.UpArrow)) {
                pos.z += speed * Time.deltaTime;
            }
            //Movement Function for moving left
            if (Input.GetKey ("s") || Input.GetKey (KeyCode.DownArrow)) {
                pos.z -= speed * Time.deltaTime;
            }
            //Movement Function for moving right
            if (Input.GetKey ("d") || Input.GetKey (KeyCode.RightArrow)) {
                pos.x += speed * Time.deltaTime;
            }
            //Movement Function for moving down
            if (Input.GetKey ("a") || Input.GetKey (KeyCode.LeftArrow)) {
                pos.x -= speed * Time.deltaTime;
            }
            transform.position = pos;
    }




}
