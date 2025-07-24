using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("PickUp");
        }
    }
}
