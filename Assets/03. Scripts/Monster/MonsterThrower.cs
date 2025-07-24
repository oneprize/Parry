using UnityEngine;

public class MonsterThrower : MonoBehaviour
{
    public Transform handTransform; // �� ��ġ
    public Rigidbody targetObject;  // ���� ��ü
    public Transform throwDirection; // ���� ���� ����

    private bool hasObject = false;

    public Transform player; // �÷��̾� ��ġ
    public float parryDistance = 3f;
    public float parryWindowTime = 0.7f;
    private float parryStartTime;
    private bool isParryWindow = false;
    private bool parryTriggered = false;

    public Material normalMaterial;
    public Material highlightMaterial;

    private Renderer targetRenderer;

    public void PickupObject()
    {
        if (targetObject == null) return;

        // �տ� ���̱�
        targetObject.transform.SetParent(handTransform);
        targetObject.transform.localPosition = Vector3.zero;
        targetObject.transform.localRotation = Quaternion.identity;

        // ���� ����
        targetObject.isKinematic = true;
        hasObject = true;
    }

    public void ThrowObject()
    {
        if (!hasObject) return;

        // �տ��� �и�
        targetObject.transform.SetParent(null);
        targetObject.isKinematic = false;

        // linearVelocity ����� ����
        Vector3 throwDir = throwDirection.forward;
        targetObject.linearVelocity = throwDir * 10f;

        // ������ ����
        targetRenderer = targetObject.GetComponent<Renderer>();
        parryTriggered = false;
        isParryWindow = false;

        hasObject = false;
    }

    void Update()
    {
        if (targetObject != null && !targetObject.isKinematic && !parryTriggered)
        {
            float distanceToPlayer = Vector3.Distance(targetObject.position, player.position);

            if (!isParryWindow && distanceToPlayer < parryDistance)
            {
                StartParryWindow();
            }

            if (isParryWindow)
            {
                float elapsed = Time.unscaledTime - parryStartTime;
                if (elapsed >= parryWindowTime)
                {
                    EndParryWindow(false);
                }
            }
        }
    }

    void StartParryWindow()
    {
        isParryWindow = true;
        parryStartTime = Time.unscaledTime;

        // �ð� ������
        Time.timeScale = 0.2f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        // ����� ���̶���Ʈ ��Ƽ���� ����
        if (targetRenderer != null && highlightMaterial != null)
        {
            targetRenderer.material = highlightMaterial;
        }

        Debug.Log("�и� Ÿ�̹�!");
    }

    void EndParryWindow(bool success)
    {
        isParryWindow = false;
        parryTriggered = true;

        // �ð� �������
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        // ��Ƽ���� ���󺹱�
        if (targetRenderer != null && normalMaterial != null)
        {
            targetRenderer.material = normalMaterial;
        }

        if (!success)
        {
            Debug.Log("�и� ����!");
        }
    }

    void FindNearestThrowable()
    {
        float radius = 3f;
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Throwable"))
            {
                targetObject = hit.GetComponent<Rigidbody>();
                break;
            }
        }
    }
}
