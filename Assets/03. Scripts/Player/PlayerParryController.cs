using UnityEngine;

public class PlayerParryController : MonoBehaviour
{
    public Animator animator;                     // Slash �ִϸ��̼ǿ�
    public GameObject CurrentParryTarget;         // ���� Ÿ�� ��
    public bool isParryAvailable = false;         // �и� Ÿ�̹� �÷���
    public GameObject breakEffectPrefab;          // �ı� ����Ʈ

    void Update()
    {
        if (isParryAvailable && Input.GetMouseButtonDown(0))
        {
            Parry();
        }
    }

    void Parry()
    {
        Debug.Log("�и� �Էµ�! Slash �ִϸ��̼� ���");

        animator.SetTrigger("Slash"); // Slash �ִϸ��̼� ���

        // �� �μ���
        if (CurrentParryTarget != null)
        {
            Vector3 pos = CurrentParryTarget.transform.position;

            if (breakEffectPrefab != null)
                Instantiate(breakEffectPrefab, pos, Quaternion.identity);

            // ������ �ı� ���
            ParryTrigger trigger = CurrentParryTarget.GetComponent<ParryTrigger>();
            if (trigger != null)
            {
                trigger.Break();
            }

            // �ʱ�ȭ
            CurrentParryTarget = null;
            isParryAvailable = false;
        }
    }
}
