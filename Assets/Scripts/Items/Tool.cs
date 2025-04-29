using UnityEngine;
using System.Collections;


public class Tool : Item, IGiveDamage
{
    public float damage;
    public Collider2D collider;
    private float timer = 0;
    private float maxTimer = 10;
    private Coroutine swingCoroutine;



    public float GiveDamage()
    {
        return damage;
    }

    public void FixedUpdate()
    {
        if (timer >= maxTimer)
        {
            if (collider.enabled) collider.enabled = false;
            timer = 0;
        }

        if (collider.enabled) timer++;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Use();
        }
        //RotateTowardsMouse();
        FlipTowardsMouse();
    }

    public void Use()
    {
        collider.enabled = true;

        // Start swing animation
        if (swingCoroutine != null)
        {
            StopCoroutine(swingCoroutine);
        }

        swingCoroutine = StartCoroutine(SwingEffect());
    }

    private void FlipTowardsMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mouseWorldPos - transform.position;

        Vector3 scale = transform.localScale;
        scale.x = -Mathf.Abs(scale.x) * (direction.x >= 0 ? 1 : -1);
        transform.localScale = scale;
    }


    private IEnumerator SwingEffect()
    {
        float duration = 0.2f;
        float swingAngle = 30f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float angle = Mathf.Sin((elapsed / duration) * Mathf.PI) * swingAngle;
            transform.localRotation = Quaternion.Euler(0f, 0f, angle);
            yield return null;
        }

        transform.localRotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Trigger enter");
        Resource giveDamage = other.GetComponent<Resource>();
        if (giveDamage != null)
        {
            giveDamage.TakeDamage(GiveDamage());
        }
    }
}
