using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles all relevatant information to each charachter, both the
/// player and the enemy AI in the game. It also allows for a charachter to
/// attack another charachter.
/// </summary>
public class Person : MonoBehaviour
{
    /// <summary>
    /// The name.
    /// </summary>
    public string Name;

    /// <summary>
    /// The attack bonus.
    /// </summary>
    public int attack_Bonus;

    /// <summary>
    /// The dexterity bonus.
    /// </summary>
    public int dexterity_Bonus;

    /// <summary>
    /// The health.
    /// </summary>
    public int health;

    /// <summary>
    /// Whether or not this person has been defeated by the player.
    /// </summary>
    public bool defeated = false;

    /// <summary>
    /// The credits.
    /// </summary>
    public int credits = 0;

    /// <summary>
    /// The battle sprite.
    /// </summary>
    public Sprite BattleSprite;

    /// <summary>
    /// The current ability of this person.
    /// </summary>
    public Ability current_Ability;

    /// <summary>
    /// Attacks the specified opponent, will draw from the persons current ability.
    /// </summary>
    /// <param name="opponent">The opponent.</param>
    /// <param name="ability_Index">Index of the ability.</param>
    /// <returns name="attackHit">Did the attack hit</returns>
    public bool attack(Person opponent, int ability_Index)
    {
        bool attackHit = false;
        switch (ability_Index)
        {
            case 1:
                current_Ability = GetComponent<Ability>();
                break;
            default:
                print("Error: No ability found");
                break;
        }

        if ((int)Random.Range(0, 100) + dexterity_Bonus > current_Ability.chance_to_miss)
        {
            opponent.health -= GetComponent<Ability>().attack_Damage;
            attackHit = true;
        }
        else
        {
            print(Name + " used " + GetComponent<Ability>().Name + " on " + opponent.Name + ":  " + "Attack missed.....");
        }

        return attackHit;
    }

    /// <summary>
    /// Gets the name of the ability. Used in the game manager class to 
    /// print the correct battle text.
    /// </summary>
    /// <returns name="current_Ability.Name">The name of the current ability</returns>
    public string getAbilityName()
    {
        return current_Ability.Name;
    }

    /// <summary>
    ///  This is not neccecary, but is included in all Unity C# scripts.
    /// </summary>
    public void Start()
    {
    }

    /// <summary>
    ///  This is not neccecary, but is included in all Unity C# scripts.
    /// </summary>
    public void Update()
    {
    }
}