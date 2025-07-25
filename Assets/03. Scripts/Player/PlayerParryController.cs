using UnityEngine;

public class PlayerParryController : MonoBehaviour
{
    public Animator animator;

    public void PlayCounterAttack()
    {
        animator.SetTrigger("ParrySlash");

        // 콤보 연결 가능 상태 → 여기서 bool로 열거나 이후 입력 체크 가능
        Invoke(nameof(EnableComboInput), 0.3f); // 타이밍 조정 가능
    }

    private void EnableComboInput()
    {
        Debug.Log("후속 콤보 입력 가능!");
        // 이후 키 입력 받으면 콤보로 넘어가는 로직 작성 가능
    }
}
