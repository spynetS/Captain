using UnityEngine;

public class Health : Resource
{
    private int maxHealth = 100; //max health of the player

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)) //if space is pressed
        {
            //Damage(10); //call damage method with 10 as argument
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            //Heal(10); //call damage method with 10 as argument
        }
    }

    public void SetHealth(int maxHealth, int health)
    {
        this.maxHealth = maxHealth;
        this.health = health;
    }
    public void Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("amount", "Amount must be greater than or equal to 0");
        }

        bool wouldOverMaxHealth = health + amount > maxHealth;
        if (wouldOverMaxHealth)
        {
            this.health = maxHealth;
        }
        else
        {
            this.health += amount;
        }

    }
}
