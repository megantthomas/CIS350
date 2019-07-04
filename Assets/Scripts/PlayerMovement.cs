using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Class controls the player movement with the arrow keys.
/// It also interacts with the SolidObject class through the GameManager class
/// to control whether the player can move through certain objects
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// The animation 
    /// </summary>
    public Animator Anim;

    /// <summary>
    /// Enum: Keeps track of what direction you last moved to determine what sprite to use
    /// </summary>
    public Direction currentDir;

    /// <summary>
    /// Holds the information of what arrow key is being pressed
    /// </summary>
    public Vector2 input;

    /// <summary>
    /// Holds whether the player is currently moving
    /// </summary>
    public bool isMoving = false;

    /// <summary>
    /// Holds the current spot of the player at any given time
    /// </summary>
    public Vector3 startPos;

    /// <summary>
    /// Holds a position in the direction the player wants to move
    /// </summary>
    public Vector3 endPos;

    /// <summary>
    /// Float used for keeping track of time
    /// </summary>
    public float t;

    /// <summary>
    /// The north sprite
    /// </summary>
    public Sprite northSprite;

    /// <summary>
    /// The east sprite
    /// </summary>
    public Sprite eastSprite;

    /// <summary>
    /// The south sprite
    /// </summary>
    public Sprite southSprite;

    /// <summary>
    /// The west sprite
    /// </summary>
    public Sprite westSprite;

    /// <summary>
    /// Public float keeping track of a multiplier for speed. 
    /// This way we can tweak how fast the player walks outside of the script
    /// </summary>
    public float walkSpeed = 3f;

    /// <summary>
    /// Controls whether the player can move at all
    /// </summary>
    public bool isAllowedToMove = true;

    /// <summary>
    /// Variables to control in what direction the player can move
    /// </summary>
    public bool canMoveNorth = true;

    /// <summary>
    /// The can move east
    /// </summary>
    public bool canMoveEast = true;

    /// <summary>
    /// The can move south
    /// </summary>
    public bool canMoveSouth = true;

    /// <summary>
    /// The can move west
    /// </summary>
    public bool canMoveWest = true;

    /// <summary>
    /// Starts this instance.
    /// </summary>
    public void Start()
    {
        isAllowedToMove = true;
        canMoveNorth = true;
        canMoveEast = true;
        canMoveSouth = true;
        canMoveWest = true;
        Anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Updates this instance. Allows player to move if an arrow key is pressed
    /// and the particular direction the player wants to move is allowed by the 
    /// game manager, ie. the player has not hit a solid object in that direction.
    /// This will only run if the player in general is allowed to move, which will
    /// be restricted in other scenes.
    /// </summary>
    public void Update()
    {
        if (!isMoving && isAllowedToMove)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                input.y = 0; 
            }
            else
            {
                input.x = 0;
            }

            if (input != Vector2.zero)
            {
                if (input.x < 0)
                {
                    currentDir = Direction.West;
                }
                else if (input.x > 0)
                {
                    currentDir = Direction.East;
                }
                else if (input.y < 0)
                {
                    currentDir = Direction.South;
                }
                else if (input.y > 0)
                {
                    currentDir = Direction.North;
                }

                switch (currentDir)
                {
                    case Direction.North:
                        gameObject.GetComponent<SpriteRenderer>().sprite = northSprite;
                        break;
                    case Direction.East:
                        gameObject.GetComponent<SpriteRenderer>().sprite = eastSprite;
                        break;
                    case Direction.South:
                        gameObject.GetComponent<SpriteRenderer>().sprite = southSprite;
                        break;
                    case Direction.West:
                        gameObject.GetComponent<SpriteRenderer>().sprite = westSprite;
                        break;
                }

                if ((input.y > 0) && (!canMoveNorth))
                {
                    isAllowedToMove = false;
                    print("hit North");
                }
                else if ((input.y < 0) && (!canMoveSouth))
                {
                    isAllowedToMove = false;
                    print("hit South");
                }
                else if ((input.x > 0) && (!canMoveEast))
                {
                    isAllowedToMove = false;
                    print("hit East");
                }
                else if ((input.x < 0) && (!canMoveWest))
                {
                    isAllowedToMove = false;
                    print("hit North");
                }

                if (!isMoving && isAllowedToMove)
                {
                    StartCoroutine(Move(transform));
                }
            }

            isAllowedToMove = true;
        }
    }

    /// <summary>
    /// Moves the player, commanded by the update method.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns name="">Returns a </returns>
    public IEnumerator Move(Transform entity)
    {
        isMoving = true;
        startPos = entity.position;
        t = 0;
        
        endPos = new Vector3(startPos.x + System.Math.Sign(input.x), startPos.y + System.Math.Sign(input.y), startPos.z);
        while (t < 1f)
        {
            t += Time.deltaTime * walkSpeed;
            entity.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        isMoving = false;
        yield return 0;
    }
}