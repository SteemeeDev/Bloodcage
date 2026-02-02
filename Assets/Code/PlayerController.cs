using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody m_rigidBody;
    [SerializeField] private Animator m_animator;
    [SerializeField] private float moveSpeed;
    [SerializeField] ChainHandler chainHandler;
    [SerializeField] Transform otherPlayer;
    [SerializeField] Transform playerModel;

    [SerializeField] private float chargeUpTime;
    [SerializeField] private ChargeUpBar chargeUpBar;

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

       // Debug.Log("d1: " + d1 + ", d2: " + d2 + ", d3: " + d3);
        transform.position -= (d1 * (0.5f * d3));
        otherPlayer.position += (d1 * (0.5f * d3));
    }

    void PullOtherPlayer(float pullForce)
    {
        otherPlayer.GetComponent<Rigidbody>().AddForce(otherPlayerDistance.normalized * pullForce);
    }

    private void Start()
    {
        chargeUpBar.maxValue = chargeUpTime;
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

        if (m_animator != null)
        {
            if (moveVelocity.magnitude > 0.1f)
            {
                m_animator.SetBool("Walking", true);
            }
            else
            {
                m_animator.SetBool("Walking", false);
            }
        }

        moveVelocity *= moveSpeed;

        m_rigidBody.AddForce(moveVelocity * Time.deltaTime);
        m_rigidBody.AddForce(moveVelocity * Time.deltaTime);

        if (otherPlayerDistance.magnitude > chainHandler.maxPlayerDistance)
        {
            FixDistanceBetweenPlayers();
        }
    }

    float chargeUp = 0;
    private void Update()
    {
        if (Input.GetKey(keybinds[4]))
        {
            chargeUp += Time.deltaTime;
            chargeUpBar.value = Mathf.Min(chargeUp, chargeUpTime);
        }
        if (Input.GetKeyUp(keybinds[4]))
        {
            if (chargeUp > chargeUpTime) chargeUp = chargeUpTime;    
            PullOtherPlayer(chargeUp / chargeUpTime * 700);
            chargeUp = 0;
            chargeUpBar.value = 0;
        }
    }

}
