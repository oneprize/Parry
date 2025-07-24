using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public Animator animator;                 // Slash 애니메이션 재생용
    public GameObject equippedWeapon;         // 현재 장착된 무기

    void Update()
    {
        // 무기를 장착한 상태이고, 마우스 왼쪽 버튼을 눌렀다면
        if (equippedWeapon != null && Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Slash");    // Slash 트리거 실행
            Debug.Log("검 휘두르기!");
        }
    }
}
