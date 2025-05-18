using UnityEngine;
using UnityEngine.UI;

public class Toggle : MonoBehaviour{

    public bool show = true;
    private CanvasGroup canvasGroup;
    public InventorySystem inventory;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();

    }

    public void ToggleIt(bool show){
        if(this.show != show){
            transform.localScale = show ? Vector3.one : Vector3.zero;
            canvasGroup.interactable = show;
            canvasGroup.blocksRaycasts = show;
            canvasGroup.alpha = show ? 1f : 0f;
            if (show){
                UpdateMenu();
            }
            this.show = show;
        }

    }

    public virtual void UpdateMenu(){}

}
