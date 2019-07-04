using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class helps manage the interactions between the other 
/// various classes of the game.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// This holds the current enemy for a battle.
    /// </summary>
    public GameObject enemy;

    /// <summary>
    /// The player camera.
    /// </summary>
    public GameObject playerCamera;

    /// <summary>
    /// The battle camera.
    /// </summary>
    public GameObject battleCamera;

    /// <summary>
    /// The end camera.
    /// </summary>
    public GameObject endCamera;

    /// <summary>
    /// The title camera.
    /// </summary>
    public GameObject titleCamera;

    /// <summary>
    /// The intro camera.
    /// </summary>
    public GameObject introCamera;

    /// <summary>
    /// The player.
    /// </summary>
    public GameObject player;

    /// <summary>
    /// The north of player invisible sprite. Used for solid object collision.
    /// </summary>
    public GameObject northOfPlayer;

    /// <summary>
    /// The east of player invisible sprite.
    /// </summary>
    public GameObject eastOfPlayer;

    /// <summary>
    /// The south of player invisible sprite.
    /// </summary>
    public GameObject southOfPlayer;

    /// <summary>
    /// The west of player invisible sprite.
    /// </summary>
    public GameObject westOfPlayer;

    /// <summary>
    /// The space counter.
    /// </summary>
    public int spaceCounter = 0;

    /// <summary>
    /// The number of lines in the intro text.
    /// Helps determine when to end the intro.
    /// </summary>
    public int endingIntroLine = 19;

    /// <summary>
    /// Boolean to keep track of if the title screen has already been shown.
    /// </summary>
    public bool titleDone = false;

    /// <summary>
    /// Boolean to keep track if the introduction has already been completed.
    /// </summary>
    public bool introDone = false;

    /// <summary>
    /// The intro text reader.
    /// </summary>
    public GameObject introTextReader;

    /// <summary>
    /// The battle text.
    /// </summary>
    public Text battleText;

    /// <summary>
    /// The ability one text.
    /// </summary>
    public Text abilityOneText;

    /// <summary>
    /// The ability two text. We never completed these extra abilities.
    /// </summary>
    public Text abilityTwoText;

    /// <summary>
    /// The ability three text. 
    /// </summary>
    public Text abilityThreeText;

    /// <summary>
    /// The ability four text.
    /// </summary>
    public Text abilityFourText;

    /// <summary>
    /// The enemy health text.
    /// </summary>
    public Text enemyHealthText;

    /// <summary>
    /// The player health text.
    /// </summary>
    public Text playerHealthText;

    /// <summary>
    /// The current ability number.
    /// </summary>
    public int current_Ability_Number;

    /// <summary>
    /// The battle enemy sprite.
    /// </summary>
    public GameObject BattleEnemySprite;

    /// <summary>
    /// The battle player sprite.
    /// </summary>
    public GameObject BattlePlayerSprite;

    /// <summary>
    /// The offset for the north sprite.
    /// </summary>
    private Vector3 offsetN;

    /// <summary>
    /// The offset east sprite.
    /// </summary>
    private Vector3 offsetE;

    /// <summary>
    /// The offset south sprite.
    /// </summary>
    private Vector3 offsetS;

    /// <summary>
    /// The offset west sprite.
    /// </summary>
    private Vector3 offsetW;

    /// <summary>
    /// The offset for the different cameras.
    /// </summary>
    private Vector3 offsetCamera1;
    private Vector3 offsetCamera2;
    private Vector3 offsetCamera3;

    /// <summary>
    /// The menu's information.
    /// </summary>
    public GameObject Menu;
    public Text MenuText;
    public bool MenuEnabled = false;
    private bool attackInProgress = false;

    /// <summary>
    /// When the game starts, this function is run. It sets the correct camera on and
    /// the others off, makes sure the player cannot move and that the menu is not
    /// displayed.
    /// </summary>
    public void Start()
    {
        endCamera.SetActive(false);
        titleCamera.SetActive(true);
        introCamera.SetActive(false);
        playerCamera.SetActive(false);
        battleCamera.SetActive(false);
        player.GetComponent<PlayerMovement>().isAllowedToMove = false;
        Menu.GetComponent<SpriteRenderer>().enabled = false;
        MenuText.GetComponent<Text>().enabled = false;
    }

    /// <summary>
    /// Every frame this method is run. It runs the intro if it is not completed.
    /// When it finishes it switches the camera to the player, and makes sure that
    /// the camera and invisible sprites follow the players movement at the correct
    /// offsets. If the player's credits get to 120, then the game finishes and 
    /// the camera switches to the end title.
    /// </summary>
    public void Update()
    {
        if (!titleDone)
        {
            SwitchToIntro();
        }
        if (!introDone)
        {
            SwitchToPlayer();
        }
        UpdateMenu();
        OpenMenu();
        // Making invisible sprites that detect collisions follow main player.
        offsetN = northOfPlayer.transform.position - player.transform.position;
        offsetE = eastOfPlayer.transform.position - player.transform.position;
        offsetS = southOfPlayer.transform.position - player.transform.position;
        offsetW = westOfPlayer.transform.position - player.transform.position;
        // Setting the offset of Camera1 to the correct postition.
        offsetCamera1 = playerCamera.transform.position - player.transform.position;
        offsetCamera2 = playerCamera.transform.position - Menu.transform.position;
        offsetCamera3 = playerCamera.transform.position - MenuText.transform.position;
        if (player.GetComponent<Person>().credits == 120)
        {
            SwitchToEndTitle();
        }
    }

    /// <summary>
    /// This happens after the update method every frame, it controls the following
    /// of the camera and invisible sprites.
    /// </summary>
    public void LateUpdate()
    {
        northOfPlayer.transform.position = player.transform.position + offsetN;
        eastOfPlayer.transform.position = player.transform.position + offsetE;
        southOfPlayer.transform.position = player.transform.position + offsetS;
        westOfPlayer.transform.position = player.transform.position + offsetW;
        Menu.transform.position = playerCamera.transform.position - offsetCamera2;
        playerCamera.transform.position = player.transform.position + offsetCamera1;
    }

    /// <summary>
    /// Switches to intro. When the player presses enter.
    /// </summary>
    public void SwitchToIntro()
    {
        if (Input.GetKeyDown(KeyCode.Return) && titleDone == false)
        {
            titleDone = true;
            titleCamera.SetActive(false);
            introCamera.SetActive(true);
        }
    }

    /// <summary>
    /// Switches to player, after the text is done reading.
    /// </summary>
    public void SwitchToPlayer()
    {
        if (titleDone == true && introDone == false)
        {
            if (introTextReader.GetComponent<TextBoxManager>().fileDoneReading)
            {
                introDone = true;
                introCamera.SetActive(false);
                playerCamera.SetActive(true);
                player.GetComponent<PlayerMovement>().isAllowedToMove = true;
            }
        }
    }

    /// <summary>
    /// Switchs to the end screen.
    /// </summary>
    void SwitchToEndTitle()
    {

        playerCamera.SetActive(false);
        endCamera.SetActive(true);
    }

    /// <summary>
    /// Updating the menu's text.
    /// </summary>
    void UpdateMenu()
    {
        MenuText.text = "Name: Student\nHP: " + player.GetComponent<Person>().health.ToString() + "\nCredits: " + player.GetComponent<Person>().credits.ToString() +
            "\nInt: " + player.GetComponent<Person>().attack_Bonus.ToString() + "\nKnowledge: " + player.GetComponent<Person>().dexterity_Bonus.ToString();
    }

    /// <summary>
    /// Displays menu to screen when 'M' is pressed.
    /// </summary>
    void OpenMenu()
    {
        if (Input.GetKeyDown(KeyCode.M) && MenuEnabled == false)
        {
            Menu.GetComponent<SpriteRenderer>().enabled = true;
            MenuText.GetComponent<Text>().enabled = true;
            MenuEnabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.M) && MenuEnabled == true)
        {
            Menu.GetComponent<SpriteRenderer>().enabled = false;
            MenuText.GetComponent<Text>().enabled = false;
            MenuEnabled = false;
        }
    }
    /// <summary>
    /// Enters the battle against the player and the current enemy held.
    /// That enemy held by the GameManager will be updated by the enemy gaze
    /// script that will run when the player makes contact with an EnemyGaze
    /// object.
    /// </summary>
    public void EnterBattle()
    {
        playerCamera.active = false;
        battleCamera.active = true;

        BattleEnemySprite.GetComponent<SpriteRenderer>().sprite = enemy.GetComponent<Person>().BattleSprite;

        player.GetComponent<PlayerMovement>().isAllowedToMove = false;
        abilityOneText.text = player.GetComponent<Ability>().Name;

        enemyHealthText.text = enemy.GetComponent<Person>().Name + "\nHP: " + enemy.GetComponent<Person>().health.ToString();
        playerHealthText.text = "HP: " + player.GetComponent<Person>().health;
        battleText.text = "";
    }

    /// <summary>
    /// Fights the specified ability number. This script is run when a button on the fight screen is pressed.
    /// It runs the Wait coroutine which has most of the actual battling code in it.
    /// </summary>
    /// <param name="ability_Number">The ability number.</param>
    public void Fight(int ability_Number)
    {
        if (attackInProgress == false)
        {
            current_Ability_Number = ability_Number;
            StartCoroutine(Wait());
        }
    }

    /// <summary>
    /// Exits the battle. Returns health to the correct stats.
    /// </summary>
    public void ExitBattle()
    {
        battleCamera.active = false;
        playerCamera.active = true;
        player.GetComponent<Person>().health = 100;
        player.GetComponent<Person>().health += 25 * (player.GetComponent<Person>().credits / 3);
        player.GetComponent<PlayerMovement>().isAllowedToMove = true;
    }

    /// <summary>
    /// This IEunerator method holds most of the battling code. First, the player's
    /// person object will attack the enemy's person object. (See Person class). Next
    /// the text on the battle screen is updated accordingly. Next the enemy attacks the
    /// player and text is updated once again. Finally, victory conditions are evaluated
    /// and correct text and camera switchin occurs if the player has either won or lost.
    /// </summary>
    /// <returns name="BattleActive"> Returns if a battle is happening</returns>
    public IEnumerator Wait()
    {
        attackInProgress = true;
        bool attackHit;
        bool BattleActive = true;
        attackHit = player.GetComponent<Person>().attack(enemy.GetComponent<Person>(), current_Ability_Number);
        battleText.text = "You used " + player.GetComponent<Person>().getAbilityName() + " on " + enemy.GetComponent<Person>().Name;

        if (!attackHit)
        {
            yield return new WaitForSecondsRealtime(.5f);
            battleText.text += "\nBut it missed...";
        }
        else
        {
            BattleEnemySprite.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSecondsRealtime(.2f);
            BattleEnemySprite.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSecondsRealtime(.2f);
            BattleEnemySprite.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSecondsRealtime(.2f);
            BattleEnemySprite.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (enemy.GetComponent<Person>().health < 0)
        {
            enemyHealthText.text = enemy.GetComponent<Person>().Name + "\nHP: 0";
        }
        else
        {
            enemyHealthText.text = enemy.GetComponent<Person>().Name + "\nHP: " + enemy.GetComponent<Person>().health.ToString();
        }

        if (enemy.GetComponent<Person>().health <= 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            battleText.text = "VICTORY: Exam Passed!!!";
            yield return new WaitForSecondsRealtime(1f);
            battleText.text = "Health increased by 25 HP!";
            yield return new WaitForSecondsRealtime(1f);
            battleText.text += "Intelligence Increased by 25!";
            yield return new WaitForSecondsRealtime(1f);
            battleText.text += "Knowledge Increased by 1!";
            yield return new WaitForSecondsRealtime(1f);
            battleText.text = "Gained 3 Credits";
            yield return new WaitForSecondsRealtime(1f);
            player.GetComponent<Person>().health = 100;
            player.GetComponent<Person>().health += 25 * (player.GetComponent<Person>().credits / 3);
            player.GetComponent<Person>().attack_Bonus += 5;
            player.GetComponent<Person>().dexterity_Bonus += 1;
            enemy.GetComponent<Person>().defeated = true;
            player.GetComponent<Person>().credits += 3;
            ExitBattle();
        }
        else if (player.GetComponent<Person>().health <= 0)
        {
            yield return new WaitForSecondsRealtime(1);
            battleText.text = "FAILURE: You Have Failed\nGo study and try again later loser";
            yield return new WaitForSecondsRealtime(2);
            ExitBattle();
        }

        if (BattleActive)
        {
            yield return new WaitForSecondsRealtime(2);
            attackHit = enemy.GetComponent<Person>().attack(player.GetComponent<Person>(), 1);
            battleText.text = enemy.GetComponent<Person>().Name + " used " + enemy.GetComponent<Person>().getAbilityName();

            if (!attackHit)
            {
                yield return new WaitForSecondsRealtime(.5f);
                battleText.text += "\nBut it missed...";
            }
            else
            {
                BattlePlayerSprite.GetComponent<SpriteRenderer>().enabled = false;
                yield return new WaitForSecondsRealtime(.2f);
                BattlePlayerSprite.GetComponent<SpriteRenderer>().enabled = true;
                yield return new WaitForSecondsRealtime(.2f);
                BattlePlayerSprite.GetComponent<SpriteRenderer>().enabled = false;
                yield return new WaitForSecondsRealtime(.2f);
                BattlePlayerSprite.GetComponent<SpriteRenderer>().enabled = true;
            }

            if (player.GetComponent<Person>().health < 0)
            {
                playerHealthText.text = "HP: 0";
            }
            else
            {
                playerHealthText.text = "HP: " + player.GetComponent<Person>().health.ToString();
            }
        }
        attackInProgress = false;
    }

    /// <summary>
    /// Exits the battle by allowing the player to move again and setting the camera to 
    /// the correct camera that the player was last at.This camera is taken as the parameter
    /// to the method.
    /// </summary>
    /// <param name="Camera">The camera.</param>
    public void ExitBattle(GameObject Camera)
    {
        battleCamera.active = false;
        Camera.active = true;
    }

    /// <summary>
    /// Solids the object hit. Locks up movement in the direction of
    /// the invisible sprite floating around the player if that sprite 
    /// makes contact with an object with the SolidObject script attached.
    /// </summary>
    /// <param name="dir">The dir.</param>
    public void SolidObjectHit(Direction dir)
    {
        switch (dir)
        {
            case Direction.North:
                player.GetComponent<PlayerMovement>().canMoveNorth = false;
                print("hit North");
                break;
            case Direction.East:
                player.GetComponent<PlayerMovement>().canMoveEast = false;
                print("hit East");
                break;
            case Direction.South:
                player.GetComponent<PlayerMovement>().canMoveSouth = false;
                print("hit South");
                break;
            case Direction.West:
                player.GetComponent<PlayerMovement>().canMoveWest = false;
                print("hit West");
                break;
        }
    }

    /// <summary>
    /// Solids the object left. 
    /// </summary>
    public void SolidObjectLeft()
    {
        player.GetComponent<PlayerMovement>().canMoveNorth = true;
        player.GetComponent<PlayerMovement>().canMoveWest = true;
        player.GetComponent<PlayerMovement>().canMoveSouth = true;
        player.GetComponent<PlayerMovement>().canMoveEast = true;
        print("Left");
    }
}
