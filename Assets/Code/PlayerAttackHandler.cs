using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] KeyCode attackButton;

    private void Update()
    {
        if (Input.GetKeyDown(attackButton))
        {
            animator.SetTrigger("Attack");
        }
    }
}
