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
                Debug.Log("패링 성공!");
                OnParrySuccess();
            }
            else
            {
                Debug.Log("패링 실패! 타이밍 늦음");
                OnParryFail();
            }
        }
    }

    public void StartParryWindow()
    {
        // "Monster" 태그나 이름으로 적을 찾음
        GameObject enemy = GameObject.FindWithTag("Enemy"); // 또는 Find("Monster")
        if (enemy != null)
        {
            StartParryWindow(enemy); // 기존 함수 호출
        }
    }

    public void StartParryWindow(GameObject enemy)
    {
        isParryWindow = true;
        parryStartTime = Time.unscaledTime;
        targetEnemy = enemy;

        // 시간 느리게
        Time.timeScale = 0.2f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        // 설정한 시간 후 시간 복원
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
