using UnityEngine;

public class YSort : MonoBehaviour
{
    protected SpriteRenderer[] spriteRenderers;

    void Awake(){
        // Get all SpriteRenderers in this object and its children
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    protected void UpdateOrder(float order=-1){
        foreach (var sr in this.spriteRenderers){
            if (sr != null){
                if (sr.transform.parent != null &&
                    sr.transform.parent.name == "Shadows_2"){
                    continue;  // Skip this iteration and go to the next SpriteRenderer
                }
                if(order == -1){
                    float yBottom = sr.bounds.min.y;
                    if(sr.transform.name == "Hand"){
                        yBottom-=1;
                    }
                    sr.sortingOrder = Mathf.RoundToInt(-yBottom * 100);
                }
                else{
                    sr.sortingOrder = Mathf.RoundToInt(order);
                }
            }
        }
    }

    void LateUpdate(){
        this.UpdateOrder();
    }
}
