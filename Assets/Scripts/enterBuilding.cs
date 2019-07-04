using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls the player moving in and out of buildings.
/// </summary>
public class enterBuilding : MonoBehaviour
{
    /// <summary>
    /// The GameObject to hold the exit point.
    /// </summary>
    public GameObject exitPoint;

    /// <summary>
    /// The player GameObject.
    /// </summary>
    public GameObject player;

    /// <summary>
    /// The Invisible sprite north of the player.
    /// </summary>
    public GameObject North;

    /// <summary>
    /// The invisible sprite east of the player.
    /// </summary>
    public GameObject East;

    /// <summary>
    /// The invisible sprite south of the player.
    /// </summary>
    public GameObject South;

    /// <summary>
    /// The invisible sprite west of the player.
    /// </summary>
    public GameObject West;

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

    /// <summary>
    /// Called when an object with the PlayerMovement script attached enters the
    /// GameObject with this script attached to it. That will then transform the
    /// position of the player and the invisible sprites to the endPosition. The 
    /// endPosition is the position of the exit point instance GameObject declared
    /// earlier.
    /// </summary>
    /// <param name="col">The col.</param>
    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.GetComponent<PlayerMovement>())
        {
            var endPosition = exitPoint.transform.position;
            var startPosition = player.transform.position;
            player.transform.position = endPosition;
            var moveDelta = player.transform.position - startPosition;
            Camera.main.transform.position += moveDelta;
            North.transform.position += moveDelta;
            East.transform.position += moveDelta;
            South.transform.position += moveDelta;
            West.transform.position += moveDelta;
        }
    }
}