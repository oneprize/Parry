using UnityEngine;

public class ParryManager : MonoBehaviour
{
    public static ParryManager Instance;

    public bool isParryWindow = false;
    private float parryStartTime;
    public float parryWindowDuration = 0.5f;
    private GameObject targetEnemy;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (isParryWindow && Input.GetButtonDown("Fire1"))
        {
            float elapsed = Time.unscaledTime - parryStartTime;

            if (elapsed <= parryWindowDuration)
            {
                Debug.Log("�и� ����!");
                OnParrySuccess();
            }
            else
            {
                Debug.Log("�и� ����! Ÿ�̹� ����");
                OnParryFail();
            }
        }
    }

    public void StartParryWindow()
    {
        // "Monster" �±׳� �̸����� ���� ã��
        GameObject enemy = GameObject.FindWithTag("Enemy"); // �Ǵ� Find("Monster")
        if (enemy != null)
        {
            StartParryWindow(enemy); // ���� �Լ� ȣ��
        }
    }

    public void StartParryWindow(GameObject enemy)
    {
        isParryWindow = true;
        parryStartTime = Time.unscaledTime;
        targetEnemy = enemy;

        // �ð� ������
        Time.timeScale = 0.2f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        // ������ �ð� �� �ð� ����
        Invoke(nameof(ResetTimeScale), parryWindowDuration);
    }

    private void ResetTimeScale()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    private void OnParrySuccess()
    {
        isParryWindow = false;
        ResetTimeScale();

        if (targetEnemy != null)
        {
            var enemy = targetEnemy.GetComponent<EnemyParryReceiver>();
            if (enemy != null)
                enemy.EnterGroggyState();
        }

        var player = FindAnyObjectByType<PlayerParryController>();
        if (player != null)
            player.PlayCounterAttack();
    }

    private void OnParryFail()
    {
        isParryWindow = false;
        ResetTimeScale();
    }
}
