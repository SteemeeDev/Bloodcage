using System.Timers;
using UnityEngine;

public class GoblinBoss : MonoBehaviour
{
    [SerializeField] GameObject[] AttackPatterns;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    float timeSinceLastAttack = 0;
    int attackIndex = 0; 

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (timeSinceLastAttack >= 8f)
        {
            timeSinceLastAttack = 0;

            Instantiate(AttackPatterns[attackIndex]);
            Debug.Log($"Attack {attackIndex} of {AttackPatterns.Length} launched");

            attackIndex++;
            attackIndex = attackIndex >= AttackPatterns.Length ? attackIndex = 0 : attackIndex;
        }
    }
}
