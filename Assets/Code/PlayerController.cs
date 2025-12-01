using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody m_rigidBody;
    [SerializeField] private float moveSpeed;

    [SerializeField] private KeyCode[] keybinds;

    float inputX;
    float inputY;

    Vector3 moveVelocity;

    public bool player1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(keybinds[0])) inputX = -1;
        if (Input.GetKey(keybinds[1])) inputX = 1;
        if (Input.GetKey(keybinds[2])) inputY = -1;
        if (Input.GetKey(keybinds[3])) inputY = 1;

        moveVelocity = new Vector3(inputX, 0, inputY);

        moveVelocity.Normalize();
        moveVelocity *= moveSpeed;

        m_rigidBody.AddForce(moveVelocity * Time.deltaTime);
    }
}
