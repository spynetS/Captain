using UnityEngine;

// meant to increase enemies dmg per wave.
public class EnemyBuffs : Enemy
{
    public float dmgIncrease = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            IncreaseDmg(dmgIncrease);
        }
    }
}
