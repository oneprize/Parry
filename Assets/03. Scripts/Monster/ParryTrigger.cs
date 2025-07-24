using UnityEngine;

public class ParryTrigger : MonoBehaviour
{
    public float parryDistance = 3f;                  // �÷��̾���� �Ÿ� ����
    public Material normalMaterial;                   // ���� ��Ƽ����
    public Material highlightMaterial;                // ����� ��Ƽ����
    public bool isParryWindow = false;

    private Renderer rend;
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (isParryWindow) return;

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist < parryDistance)
        {
            StartParryWindow();
        }
    }

    void StartParryWindow()
    {
        isParryWindow = true;

        // ��Ƽ���� ����
        if (highlightMaterial != null && rend != null)
        {
            rend.material = highlightMaterial;
        }

        // �÷��̾�� �˸�
        PlayerParryController playerParry = player.GetComponent<PlayerParryController>();
        if (playerParry != null)
        {
            playerParry.CurrentParryTarget = this.gameObject;
            playerParry.isParryAvailable = true;
        }
    }

    // �� ���� �Լ� (�ܺο��� ȣ���)
    public void Break()
    {
        Destroy(gameObject);
    }
}
