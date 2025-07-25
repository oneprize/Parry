using UnityEngine;

public class ParryEffectHandler : MonoBehaviour
{
    [Header("���͸��� ����")]
    public Material normalMaterial;          // ���� ���͸���
    public Material parryMaterial;           // �и� Ÿ�̹� ���� �� ���͸���

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();

        // ���� �� ���� ���͸���� ����
        if (rend != null && normalMaterial != null)
        {
            rend.material = normalMaterial;
        }
    }

    public void ActivateParryEffect()
    {
        if (rend != null && parryMaterial != null)
        {
            rend.material = parryMaterial;
        }

        Debug.Log("�и� Ÿ�̹� ����!");
    }

    public void ResetMaterial()
    {
        if (rend != null && normalMaterial != null)
        {
            rend.material = normalMaterial;
        }
    }
}
