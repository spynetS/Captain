using UnityEngine;

public class YSort : MonoBehaviour
{
    private SpriteRenderer[] spriteRenderers;

    void Awake()
    {
        // Get all SpriteRenderers in this object and its children
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void LateUpdate()
    {
        foreach (var sr in spriteRenderers)
        {
            if (sr != null)
            {
				if (sr.transform.parent != null && sr.transform.parent.name == "Shadows_2")
                {
                    continue;  // Skip this iteration and go to the next SpriteRenderer
                }
                float yBottom = sr.bounds.min.y;
                sr.sortingOrder = Mathf.RoundToInt(-yBottom * 100);
            }
        }
    }
}
