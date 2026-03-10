using UnityEngine;

public class Block : MonoBehaviour
{
    [Header("Break effect")]
    [Tooltip("Assign prefab that contains the break animation (Animator).")]
    public GameObject breakEffectPrefab;

    // Optional fallback if BreakEffect ikke kan finde animationens længde:
    [Tooltip("Fallback duration (seconds) if effect can't auto-detect clip length.")]
    public float fallbackEffectDuration = 1.0f;



    void Update()
    {
        if (transform.position.y < -4f)
        {
            SpawnBreakEffect();
            Destroy(gameObject);
        }


    }

    void SpawnBreakEffect()
    {
        if (breakEffectPrefab == null) return;

        // Instantiate ved blockens position (z sættes så animation ligger synligt)
        Vector3 spawnPos = transform.position;
        spawnPos.y += 0.31f;
        // Hvis du arbejder i 2D, kan du sikre z = 0 eller -1 så den vises over/under korrekt
        spawnPos.z = 0f;

        GameObject effect = Instantiate(breakEffectPrefab, spawnPos, Quaternion.identity);

        // Hvis prefab'en ikke selv fjerner sig, kan vi give den en fallback-destruction:
        var be = effect.GetComponent<BreakEffect>();
        if (be == null)
        {
            // hvis BreakEffect-scriptet ikke er sat på prefab, så destroy efter fallback
            Destroy(effect, fallbackEffectDuration);
        }
    }

}