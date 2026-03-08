using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BreakEffect : MonoBehaviour
{
    [Tooltip("Extra padding (seconds) added to detected animation length.")]
    public float extraPadding = 0.05f;

    void Start()
    {
        Animator anim = GetComponent<Animator>();
        if (anim == null)
        {
            // Ingen animator -> bare fjern efter 1 sekund
            Destroy(gameObject, 1f);
            return;
        }

        // Prøv at aflæse klip-længder fra runtime controller
        var controller = anim.runtimeAnimatorController;
        if (controller == null || controller.animationClips == null || controller.animationClips.Length == 0)
        {
            Destroy(gameObject, 1f);
            return;
        }

        // Find længste clip i controller (sikrer at vi har hele animationen)
        float maxLength = 0f;
        foreach (var clip in controller.animationClips)
        {
            if (clip.length > maxLength) maxLength = clip.length;
        }

        // Destroy objektet efter clip-længden + padding
        Destroy(gameObject, maxLength + extraPadding);
    }
}