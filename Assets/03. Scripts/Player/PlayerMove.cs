using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;

    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool run = Input.GetKey(KeyCode.LeftShift);
        bool jump = Input.GetKeyDown(KeyCode.Space);

        // 애니메이터 파라미터 설정
        animator.SetFloat("Horizontal", h);
        animator.SetFloat("Vertical", v);
        animator.SetBool("IsRunning", run);
        animator.SetBool("IsJumping", jump);

        // 실제 이동 처리
        Vector3 move = new Vector3(h, 0, v).normalized;
        float speed = run ? runSpeed : moveSpeed;
        transform.Translate(move * speed * Time.deltaTime, Space.World);

        // 점프 처리
        if (jump && Mathf.Abs(rb.linearVelocity.y) < 0.01f) // 바닥에 있을 때만 점프
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}