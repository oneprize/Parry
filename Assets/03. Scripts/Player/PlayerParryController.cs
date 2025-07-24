using UnityEngine;

public class PlayerParryController : MonoBehaviour
{
    public Animator animator;                     // Slash 애니메이션용
    public GameObject CurrentParryTarget;         // 현재 타겟 돌
    public bool isParryAvailable = false;         // 패링 타이밍 플래그
    public GameObject breakEffectPrefab;          // 파괴 이펙트

    void Update()
    {
        if (isParryAvailable && Input.GetMouseButtonDown(0))
        {
            Parry();
        }
    }

    void Parry()
    {
        Debug.Log("패링 입력됨! Slash 애니메이션 재생");

        animator.SetTrigger("Slash"); // Slash 애니메이션 재생

        // 돌 부수기
        if (CurrentParryTarget != null)
        {
            Vector3 pos = CurrentParryTarget.transform.position;

            if (breakEffectPrefab != null)
                Instantiate(breakEffectPrefab, pos, Quaternion.identity);

            // 돌에게 파괴 명령
            ParryTrigger trigger = CurrentParryTarget.GetComponent<ParryTrigger>();
            if (trigger != null)
            {
                trigger.Break();
            }

            // 초기화
            CurrentParryTarget = null;
            isParryAvailable = false;
        }
    }
}
