using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is placed the invisible sprite to the north of the player. 
/// It is used for collision handling, it does nothing. Its only purpose
/// is to be attached to a game object so the GameManager class can detect
/// what particular Script is attached to an invisible sprite that made 
/// contact with a GameObject with the solid object script attached.
/// </summary>
public class northOfPlayer : MonoBehaviour
{
    /// <summary>
    ///  This is not neccecary, but is included in all Unity C# scripts.
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