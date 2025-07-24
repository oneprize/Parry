using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    public Transform weaponHoldPoint; // �� ��ġ
    private GameObject currentWeapon;
    public GameObject breakEffectPrefab; // ����Ʈ ������
    public bool isParryActive = false;   // �и� Ÿ�̹� �÷��� (MonsterThrower���� True�� ������)

    // �ִϸ��̼� �̺�Ʈ���� �� �Լ� ȣ��
    public void AttachWeapon()
    {
        if (currentWeapon == null) return;

        // ���� ���̱�
        currentWeapon.transform.SetParent(weaponHoldPoint);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;

        // ���� ����
        Rigidbody rb = currentWeapon.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        Collider col = currentWeapon.GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Debug.Log("���� ���� �Ϸ�!");
    }

    // ���� �� ���� Ž���ؼ� currentWeapon�� ����
    public void FindNearbyWeapon()
    {
        float radius = 2f;
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Weapon"))
            {
                currentWeapon = hit.gameObject;
                Debug.Log("���� �߰�: " + currentWeapon.name);
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isParryActive) return;

        if (other.CompareTag("Throwable"))
        {
            Debug.Log("���� ���� ����! �и� ����!");

            // ����Ʈ ����
            if (breakEffectPrefab != null)
            {
                Instantiate(breakEffectPrefab, other.transform.position, Quaternion.identity);
            }

            // �� ����
            Destroy(other.gameObject);

            // ���� �ʱ�ȭ (���ϸ� ��� Ȱ��ȭ�ص� ��)
            isParryActive = false;
        }
    }
}
