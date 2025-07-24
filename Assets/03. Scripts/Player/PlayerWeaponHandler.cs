using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    public Transform weaponHoldPoint; // 손 위치
    private GameObject currentWeapon;
    public GameObject breakEffectPrefab; // 이펙트 프리팹
    public bool isParryActive = false;   // 패링 타이밍 플래그 (MonsterThrower에서 True로 설정됨)

    // 애니메이션 이벤트에서 이 함수 호출
    public void AttachWeapon()
    {
        if (currentWeapon == null) return;

        // 무기 붙이기
        currentWeapon.transform.SetParent(weaponHoldPoint);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;

        // 물리 제거
        Rigidbody rb = currentWeapon.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        Collider col = currentWeapon.GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Debug.Log("무기 장착 완료!");
    }

    // 범위 내 무기 탐색해서 currentWeapon에 저장
    public void FindNearbyWeapon()
    {
        float radius = 2f;
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Weapon"))
            {
                currentWeapon = hit.gameObject;
                Debug.Log("무기 발견: " + currentWeapon.name);
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isParryActive) return;

        if (other.CompareTag("Throwable"))
        {
            Debug.Log("검이 돌과 접촉! 패링 성공!");

            // 이펙트 생성
            if (breakEffectPrefab != null)
            {
                Instantiate(breakEffectPrefab, other.transform.position, Quaternion.identity);
            }

            // 돌 제거
            Destroy(other.gameObject);

            // 상태 초기화 (원하면 계속 활성화해도 됨)
            isParryActive = false;
        }
    }
}
