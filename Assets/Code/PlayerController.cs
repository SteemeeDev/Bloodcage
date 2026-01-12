using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody m_rigidBody;
    [SerializeField] private float moveSpeed;
    [SerializeField] ChainHandler chainHandler;
    [SerializeField] Transform otherPlayer;
    [SerializeField] Transform playerModel;

    [SerializeField] private KeyCode[] keybinds;

    float inputX;
    float inputY;

    Vector3 moveVelocity;
    Vector3 otherPlayerDistance;

    public bool player1;

    
    Vector3 m_Gravity = new Vector3(0, -4, 0);
    float m_DistanceBetweenNodes = 0.5f;
    int m_ConstraintIterationCount = 4;

    public Vector3 Position;
    public Vector3 OtherNewPosition;
    public Vector3 PreviousPosition = Vector3.zero;
    public Vector3 OtherPreviousPosition = Vector3.zero;

    Vector3 NewPosition;

    private void CalculateNewPosition()
    {
        var newPreviousPosition = transform.position;
        var newOtherPreviousPostion = otherPlayer.transform.position;

        NewPosition = (2 * transform.position) - PreviousPosition +
                                (m_rigidBody.linearVelocity * (float)Math.Pow(Time.fixedDeltaTime, 2));
        OtherNewPosition = (2 * otherPlayer.transform.position) - OtherPreviousPosition +
                        (otherPlayer.GetComponent<Rigidbody>().linearVelocity * (float)Math.Pow(Time.fixedDeltaTime, 2));
        PreviousPosition = newPreviousPosition;
        OtherPreviousPosition = newOtherPreviousPostion;
    }
    private void FixDistanceBetweenPlayers()    //Applying constraints
    {
        CalculateNewPosition();

        var d1 = transform.position - otherPlayer.position;
        var d2 = d1.magnitude;
        var d3 = (d2 - chainHandler.maxPlayerDistance) / d2;

        Debug.Log("d1: " + d1 + ", d2: " + d2 + ", d3: " + d3);
        transform.position -= (d1 * (0.5f * d3));
        otherPlayer.position += (d1 * (0.5f * d3));
    }

    void PullOtherPlayer()
    {
        otherPlayer.GetComponent<Rigidbody>().AddForce(otherPlayerDistance.normalized*200);
    }

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

        otherPlayerDistance = transform.position - otherPlayer.position;

        moveVelocity = new Vector3(inputX, 0, inputY);
        moveVelocity.Normalize();

        playerModel.LookAt(transform.position + m_rigidBody.linearVelocity);

        moveVelocity *= moveSpeed;

        m_rigidBody.AddForce(moveVelocity * Time.deltaTime);
        m_rigidBody.AddForce(moveVelocity * Time.deltaTime);

        if (otherPlayerDistance.magnitude > chainHandler.maxPlayerDistance)
        {
            FixDistanceBetweenPlayers();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(keybinds[4])) PullOtherPlayer();
    }

}
