using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class controls the "enemy gaze" which triggers a battle. 
/// This script will be attached to invisible cubes extending from 
/// enemy GameObjects representing their field of vision.
/// </summary>
public class EnemyGaze : MonoBehaviour
{
    /// <summary>
    /// This is the particular enemy GameObject that coorisponds
    /// to this particular Enemy Gaze.
    /// </summary>
    public GameObject enemy;

    /// <summary>
    /// This is going to be the GameManager invisible object
    /// that holds the GameManager script. EnemyGaze must 
    /// communicate with the GameManager to initiate a battle.
    /// </summary>
    public GameManager gm;

    /// <summary>
    /// At the start of the game, this method will initialize the correct script as
    /// the gm (which is the GameManager Script).
    /// </summary>
    public void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    /// <summary>
    /// Every frame, it is checked to see if the Enemy has been defeated,
    /// if so the GameObject that this script is attached to will be destroyed.
    /// That makes it so you cannot fight an enemy you have previously defeated.
    /// </summary>
    public void Update()
    {
        if (enemy.GetComponent<Person>().defeated)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// If a GameObject collides with this object and has the playerMovement 
    /// class attached to it, we know the player has entered the gaze and a
    /// battle must be initiated. The game manager sets the current enemy it
    /// holds to the enemy held in this particular EnemyGaze script and then 
    /// enters a battle.
    /// </summary>
    /// <param name="col">The col.</param>
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerMovement>())
        {
            gm.GetComponent<GameManager>().enemy = enemy;
            gm.EnterBattle();
        }
    }
}
