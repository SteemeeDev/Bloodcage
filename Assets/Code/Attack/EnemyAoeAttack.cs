using System.Reflection;
using UnityEngine;

public class EnemyAoeAttack : MonoBehaviour
{
    [SerializeField] Material mat;

    [SerializeField] float chargeUpTime = 0;

    float aliveTime = 0;


    // Update is called once per frame
    public virtual void Update()
    {
        aliveTime += Time.deltaTime;

        mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 1f - Mathf.Sqrt(1f - Mathf.Pow((aliveTime / chargeUpTime), 2)));

        if (aliveTime > chargeUpTime)
        {
            Destroy(gameObject);
        }
    }
}
