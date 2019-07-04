using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class controls how text is displayed on the screen for the intro.
/// </summary>
public class TextBoxManager : MonoBehaviour
{
    /// <summary>
    /// The text box.
    /// </summary>
    public GameObject textBox;

    /// <summary>
    /// The text.
    /// </summary>
    public Text theText;

    /// <summary>
    /// The text file.
    /// </summary>
    public TextAsset textFile;

    /// <summary>
    /// The text lines. Used for parsing.
    /// </summary>
    public string[] textLines;

    /// <summary>
    /// The current line, keeps track of the position in the textLine array we are in.
    /// </summary>
    public int currentLine = 0;

    /// <summary>
    /// The end at line.
    /// </summary>
    public int endAtLine;

    /// <summary>
    /// The printing text. Doesnt allow for skipping lines by spamming space.
    /// </summary>
    public bool printingText = false;

    /// <summary>
    /// The temporary string, used in parsing.
    /// </summary>
    public char[] tempString;

    /// <summary>
    /// The file done reading boolean, determines when to switch to the actual game ending the intro.
    /// </summary>
    public bool fileDoneReading = false;

    /// <summary>
    /// Starts this instance. Parses the text file into textLines.
    /// </summary>
    public void Start()
    {
        if (textFile != null)
        {
            textLines = textFile.text.Split('\n');
        }

        endAtLine = textLines.Length;
    }

    /// <summary>
    /// Updates this instance. If space is pressed, another single line of TextLines
    /// is printed slowly to the screen.
    /// </summary>
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !printingText)
        {
            if (currentLine >= endAtLine)
            {
                fileDoneReading = true;
            }
            else
            {
                StartCoroutine(Wait());
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !fileDoneReading)
        {
            fileDoneReading = true;
        }
    }

    /// <summary>
    /// Prints out a line to the screen.
    /// </summary>
    /// <returns name="WaitForSecondsRealTime">How long to wait between letter displays</returns>
    public IEnumerator Wait()
    {
        printingText = true;
        theText.text = textLines[currentLine][0].ToString();
        for (int i = 1; i < 100; i++)
        {
            if (i < textLines[currentLine].Length)
            {
                yield return new WaitForSecondsRealtime(.03f);
                theText.text += textLines[currentLine][i].ToString();
                if (textLines[currentLine][i] == '\n')
                {
                    break;
                }
            }
        }

        theText.text += "\n";
        currentLine++;
        theText.text += textLines[currentLine][0].ToString();
        for (int i = 1; i < 100; i++)
        {
            if (i < textLines[currentLine].Length)
            {
                yield return new WaitForSecondsRealtime(.03f);
                theText.text += textLines[currentLine][i].ToString();
                if (textLines[currentLine][i] == '\n')
                {
                    break;
                }
            }
        }

        currentLine++;
        yield return new WaitForSecondsRealtime(.03f);
        printingText = false;
    }
}
