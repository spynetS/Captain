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
        if(Input.GetKeyDown(KeyCode.H)) //if space is pressed
        {
            //Heal(10); //call damage method with 10 as argument
        }
    }

    public void SetHealth(int maxHealth, int health)
    {
        this.maxHealth = maxHealth; //set max health to max health
        this.health = health; //set health to health
    }
    public void Heal(int amount)
    {
        if (amount < 0) //if amount is less than 0
        {
            throw new System.ArgumentOutOfRangeException("amount", "Amount must be greater than or equal to 0"); //throw exception
        }

        bool wouldOverMaxHealth = health + amount > maxHealth; //check if health + amount is greater than max health
        if (wouldOverMaxHealth) //if health + amount is greater than max health
        {
            this.health = maxHealth; //set health to max health
        }
        else
        {
            this.health += amount; //increase health by amount
        }

    }
}
