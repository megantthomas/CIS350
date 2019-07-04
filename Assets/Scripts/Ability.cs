using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls data for the Ability script that keeps track of
/// relevant information represting a charachters in battle abilities.
/// </summary>
public class Ability : MonoBehaviour
{
    /// <summary>
    /// The name of the ability.
    /// </summary>
    public string Name;

    /// <summary>
    /// The attack damage. 
    /// </summary>
    public int attack_Damage;

    /// <summary>
    /// The chance to miss.
    /// </summary>
    public int chance_to_miss;

    /// <summary>
    /// This is not neccecary, but is included in all Unity C# scripts.
    /// </summary>
    public void Start()
    {
    }

    /// <summary>
    /// This is not neccecary, but is included in all Unity C# scripts.
    /// </summary>
    public void Update()
    {
    }
}