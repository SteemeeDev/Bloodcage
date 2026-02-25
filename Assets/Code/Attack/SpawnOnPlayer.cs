using UnityEditor.Timeline;
using UnityEngine;

public class SpawnOnPlayer : EnemyAoeAttack
{
    private void Start()
    {
        string playertag = Random.Range(0, 2) == 0 ? "Player1" : "Player2";
        Vector3 playerpos = GameObject.FindGameObjectWithTag(playertag).transform.position;
        transform.position = new Vector3(
            playerpos.x,
            0f,
            playerpos.z
        );

    }

    public override void Update()
    {
        base.Update();
    }
}
