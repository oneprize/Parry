using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyWeapon : MonoBehaviour
{
    public MonsterAI monsterAI;

    public bool damageWindow { get; private set; } = false;
    public bool parriedThisSwing { get; private set; } = false;

    public void BeginAttackWindow()
    {
        damageWindow = true;
        parriedThisSwing = false;
    }

    public void EndAttackWindow()
    {
        damageWindow = false;
    }

    public void OnParried()
    {
        if (parriedThisSwing) return;
        parriedThisSwing = true;
        damageWindow = false;
        if (monsterAI != null)
            monsterAI.EnterGroggy();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!damageWindow || parriedThisSwing) return;
        if (!other.CompareTag("Player")) return;

        // 0) 패링 성공 직후 i-frame이면 무시
        if (ParryManager.Instance != null && ParryManager.Instance.IsParryInvulnerable())
        {
            return;
        }

        // 1) 패링 창인데 입력 없이 맞음 → 실패 처리
        if (ParryManager.Instance != null && ParryManager.Instance.isParryWindow)
        {
            ParryManager.Instance.FailParryDueToHit();
            damageWindow = false;
            return;
        }

        // 2) 일반 피격
        var hitRecv = other.GetComponentInParent<PlayerHitReceiver>();
        if (hitRecv != null)
        {
            hitRecv.TriggerKnockdown();
            damageWindow = false;
        }
    }
}
