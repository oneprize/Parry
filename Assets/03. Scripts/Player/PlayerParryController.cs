using UnityEngine;

public class PlayerParryController : MonoBehaviour
{
    public Animator animator;

    public void PlayCounterAttack()
    {
        animator.SetTrigger("ParrySlash");

        // �޺� ���� ���� ���� �� ���⼭ bool�� ���ų� ���� �Է� üũ ����
        Invoke(nameof(EnableComboInput), 0.3f); // Ÿ�̹� ���� ����
    }

    private void EnableComboInput()
    {
        Debug.Log("�ļ� �޺� �Է� ����!");
        // ���� Ű �Է� ������ �޺��� �Ѿ�� ���� �ۼ� ����
    }
}
