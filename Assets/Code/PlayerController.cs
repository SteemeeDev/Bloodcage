using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody m_rigidBody;
    [SerializeField] private float moveSpeed;
    [SerializeField] ChainHandler chainHandler;

    [SerializeField] private KeyCode[] keybinds;

    float inputX;
    float inputY;

    Vector3 moveVelocity;
    Vector3 otherPlayerDistance;

    public bool player1;

    // Update is called once per frame
    void FixedUpdate()
    {
        // X movement
        if      (Input.GetKey(keybinds[0])) inputX = -1;
        else if (Input.GetKey(keybinds[1])) inputX = 1;
        else    inputX = 0;

        // Y movement
        if      (Input.GetKey(keybinds[2])) inputY = -1;
        else if (Input.GetKey(keybinds[3])) inputY = 1;
        else    inputY = 0;

        if (player1) otherPlayerDistance = transform.position - chainHandler.player2.position;
        else otherPlayerDistance = transform.position - chainHandler.player1.position;
        


        moveVelocity = new Vector3(inputX, 0, inputY);

        moveVelocity.Normalize();
        moveVelocity *= moveSpeed;

        m_rigidBody.AddForce(moveVelocity * Time.deltaTime);
    }
}
