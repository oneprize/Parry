using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public Animator animator;                 // Slash �ִϸ��̼� �����
    public GameObject equippedWeapon;         // ���� ������ ����
    public GameObject gunWeapon;

    void Update()
    {
        // ���⸦ ������ �����̰�, ���콺 ���� ��ư�� �����ٸ�
        if (equippedWeapon != null && Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Slash");    // Slash Ʈ���� ����
            Debug.Log("�� �ֵθ���!");
        }

        // ���⸦ ������ �����̰�, ���콺 ���� ��ư�� �����ٸ�
        if (gunWeapon != null && Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Gunfire");    // Slash Ʈ���� ����
            Debug.Log("�� !");
        }
    }
}
