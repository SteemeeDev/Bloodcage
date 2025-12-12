using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainHandler : MonoBehaviour
{
    [SerializeField] LineRenderer chainRenderer;
    public Transform player1;
    public Transform player2;

    public float playerDistance;
    public float maxPlayerDistance;

    // Update is called once per frame
    void FixedUpdate()
    {
        playerDistance = Vector3.Distance(player1.position, player2.position);

        chainRenderer.positionCount = 2;
        chainRenderer.SetPosition(0, player1.position);
        chainRenderer.SetPosition(1, player2.position);
    }

    private void Update()
    {
        if (playerDistance > maxPlayerDistance)
        {
            chainRenderer.startColor = Color.red;
            chainRenderer.endColor = Color.red;
        }
        else
        {
            chainRenderer.startColor = Color.white;
            chainRenderer.endColor = Color.white;
        }
    }
}
