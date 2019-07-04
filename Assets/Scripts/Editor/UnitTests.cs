using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// This class holds all the unit test 
/// </summary>
public class NewEditModeTest
{
    /// <summary>
    /// Tests the person construction.
    /// </summary>
    [Test]
    public void TestPersonConstruction()
    {
        var player = new Person();

        player.Name = "Nate";
        player.attack_Bonus = 5;
        player.dexterity_Bonus = 4;
        player.health = 100;

        Assert.AreEqual("Nate", player.Name);
        Assert.AreEqual(5, player.attack_Bonus);
        Assert.AreEqual(4, player.dexterity_Bonus);
        Assert.AreEqual(100, player.health);
    }

    /// <summary>
    /// Tests the game manager construction.
    /// </summary>
    [Test]
    public void TestGameManagerConstruction()
    {
        var gm = new GameManager();

        Assert.AreEqual(0, gm.spaceCounter);
        Assert.AreEqual(19, gm.endingIntroLine);
        Assert.IsFalse(gm.titleDone);
        Assert.IsFalse(gm.introDone);
    }

    /// <summary>
    /// Tests the solid object left code.
    /// </summary>
    [Test]
    public void testSolidObjectLeftCode()
    {
        var player = new GameObject();
        player.AddComponent<Person>();
        player.GetComponent<Person>().Name = "Nate";

        Assert.AreEqual("Nate", player.GetComponent<Person>().Name);
    }

    /// <summary>
    /// Tests the enemy gaze self destruct.
    /// </summary>
    [Test]
    public void testEnemyGazeSelfDestruct()
    {
        var gm = new GameObject();
        gm.AddComponent<GameManager>();
        var gaze = new GameObject();
        gaze.AddComponent<EnemyGaze>();
        var enemy = new GameObject();
        enemy.AddComponent<Person>();

        gaze.GetComponent<EnemyGaze>().enemy = enemy;
        gaze.GetComponent<EnemyGaze>().gm = gm.GetComponent<GameManager>();

        enemy.GetComponent<Person>().defeated = false;

        if (enemy.GetComponent<Person>().defeated)
        {
            gm = null;
        }

        Assert.AreNotEqual(null, gm);

        enemy.GetComponent<Person>().defeated = true;

        if (enemy.GetComponent<Person>().defeated)
        {
            gm = null;
        }

        Assert.AreEqual(null, gm);
    }

    /// <summary>
    /// Tests the game manager collision detection.
    /// </summary>
    [Test]
    public void testGameManagerCollisionDetection()
    {
        var gm = new GameObject();
        gm.AddComponent<GameManager>();
        var gaze = new GameObject();
        gaze.AddComponent<EnemyGaze>();
        var player = new GameObject();
        player.AddComponent<Person>();

        gaze.GetComponent<EnemyGaze>().enemy = player;
        gaze.GetComponent<EnemyGaze>().gm = gm.GetComponent<GameManager>();

        Assert.AreEqual(null, player.GetComponent<PlayerMovement>());
    }

    /// <summary>
    /// Tests the attack.
    /// </summary>
    [Test]
    public void testAttack()
    {
        var attacker = new GameObject();
        attacker.AddComponent<Person>();
        var defender = new GameObject();
        defender.AddComponent<Person>();
        var ability = new GameObject();
        ability.AddComponent<Ability>();
        ability.GetComponent<Ability>().attack_Damage = 100;
        ability.GetComponent<Ability>().chance_to_miss = 100;
        attacker.GetComponent<Person>().current_Ability = ability.GetComponent<Ability>();
        attacker.GetComponent<Person>().attack_Bonus = 0;
        attacker.GetComponent<Person>().dexterity_Bonus = 0;
        defender.GetComponent<Person>().dexterity_Bonus = 0;
        defender.GetComponent<Person>().health = 100;
        ability.GetComponent<Ability>().Name = "Name";

        attacker.GetComponent<Person>().attack(defender.GetComponent<Person>(), 1);
        Assert.AreEqual(100, defender.GetComponent<Person>().health);
    }

    /// <summary>
    /// Tests the ability name fetching.
    /// </summary>
    [Test]
    public void testAbilityNameFetching()
    {
        var ability = new GameObject();
        ability.AddComponent<Ability>().Name = "Name";

        Assert.AreEqual("Name", ability.GetComponent<Ability>().Name);
    }

    /// <summary>
    /// Tests the ability attack damage fetching.
    /// </summary>
    [Test]
    public void testAbilityAttackDamageFetching()
    {
        var ability = new GameObject();
        ability.AddComponent<Ability>().attack_Damage = 5;

        Assert.AreEqual(5, ability.GetComponent<Ability>().attack_Damage);
    }

    /// <summary>
    /// Tests the ability chance to miss fetching.
    /// </summary>
    [Test]
    public void testAbilityChanceToMissFetching()
    {
        var ability = new GameObject();
        ability.AddComponent<Ability>().chance_to_miss = 5;

        Assert.AreEqual(5, ability.GetComponent<Ability>().chance_to_miss);
    }

    /// <summary>
    /// Tests the solid object collision north.
    /// </summary>
    [Test]
    public void testSolidObjectColilisionNorth()
    {
        var solidObject = new GameObject();
        solidObject.AddComponent<SolidObject>();
        var gm = new GameObject();
        gm.AddComponent<GameManager>();
        solidObject.GetComponent<SolidObject>().gm = gm.GetComponent<GameManager>();
        var player = new GameObject();
        player.AddComponent<PlayerMovement>();
        gm.GetComponent<GameManager>().player = player;

        gm.GetComponent<GameManager>().SolidObjectHit(Direction.North);

        Assert.AreEqual(false, player.GetComponent<PlayerMovement>().canMoveNorth);
    }

    /// <summary>
    /// Tests the solid object collision east.
    /// </summary>
    [Test]
    public void testSolidObjectColilisionEast()
    {
        var solidObject = new GameObject();
        solidObject.AddComponent<SolidObject>();
        var gm = new GameObject();
        gm.AddComponent<GameManager>();
        solidObject.GetComponent<SolidObject>().gm = gm.GetComponent<GameManager>();
        var player = new GameObject();
        player.AddComponent<PlayerMovement>();
        gm.GetComponent<GameManager>().player = player;

        gm.GetComponent<GameManager>().SolidObjectHit(Direction.East);

        Assert.AreEqual(false, player.GetComponent<PlayerMovement>().canMoveEast);
    }

    /// <summary>
    /// Tests the solid object collision south.
    /// </summary>
    [Test]
    public void testSolidObjectColilisionSouth()
    {
        var solidObject = new GameObject();
        solidObject.AddComponent<SolidObject>();
        var gm = new GameObject();
        gm.AddComponent<GameManager>();
        solidObject.GetComponent<SolidObject>().gm = gm.GetComponent<GameManager>();
        var player = new GameObject();
        player.AddComponent<PlayerMovement>();
        gm.GetComponent<GameManager>().player = player;

        gm.GetComponent<GameManager>().SolidObjectHit(Direction.South);

        Assert.AreEqual(false, player.GetComponent<PlayerMovement>().canMoveSouth);
    }

    /// <summary>
    /// Tests the solid object collision west.
    /// </summary>
    [Test]
    public void testSolidObjectColilisionWest()
    {
        var solidObject = new GameObject();
        solidObject.AddComponent<SolidObject>();
        var gm = new GameObject();
        gm.AddComponent<GameManager>();
        solidObject.GetComponent<SolidObject>().gm = gm.GetComponent<GameManager>();
        var player = new GameObject();
        player.AddComponent<PlayerMovement>();
        gm.GetComponent<GameManager>().player = player;

        gm.GetComponent<GameManager>().SolidObjectHit(Direction.West);

        Assert.AreEqual(false, player.GetComponent<PlayerMovement>().canMoveWest);
    }

    /// <summary>
    /// Tests the solid object left.
    /// </summary>
    [Test]
    public void testSolidObjectLeft()
    {
        var gm = new GameObject();
        gm.AddComponent<GameManager>();
        var player = new GameObject();
        player.AddComponent<Player>();
        gm.GetComponent<GameManager>().player = player;
        player.AddComponent<PlayerMovement>();

        player.GetComponent<PlayerMovement>().canMoveNorth = false;
        player.GetComponent<PlayerMovement>().canMoveEast = false;
        player.GetComponent<PlayerMovement>().canMoveSouth = false;
        player.GetComponent<PlayerMovement>().canMoveWest = false;

        gm.GetComponent<GameManager>().SolidObjectLeft();

        Assert.AreEqual(true, player.GetComponent<PlayerMovement>().canMoveNorth);
        Assert.AreEqual(true, player.GetComponent<PlayerMovement>().canMoveEast);
        Assert.AreEqual(true, player.GetComponent<PlayerMovement>().canMoveWest);
        Assert.AreEqual(true, player.GetComponent<PlayerMovement>().canMoveSouth);
    }

    /// <summary>
    /// Tests the text box manager instantiation.
    /// </summary>
    [Test]
    public void TestTextBoxManagerInstantiation()
    {
        var box = new GameObject();
        box.AddComponent<TextBoxManager>();

        Assert.AreEqual(false, box.GetComponent<TextBoxManager>().fileDoneReading);
        Assert.AreEqual(false, box.GetComponent<TextBoxManager>().printingText);
        Assert.AreEqual(0, box.GetComponent<TextBoxManager>().currentLine);
    }
}
