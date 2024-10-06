using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    //Text Component
    public TextMeshPro Text;
    //The SpriteRenderer that draws the speaking character
    public SpriteRenderer Character;
    //The SpriteRenderer that draws the background
    public SpriteRenderer Background;

    //A list of all the character sprites
    //I need to make this variables so I can reference them
    public Sprite GaryDefault;
    public Sprite GaryFrown;
    public Sprite GaryWink;
    public Sprite VampDefault;
    public Sprite VampWink;
    
    //A list of all the background sprites
    public Sprite OutsideBG;
    public Sprite InsideBG;
    
    //A list of all the lines of dialogue
    //These will be read out by the characters in order
    //DialogueLine is a custom class at the bottom of this script
    public List<DialogueLine> Lines;
    //Which line of dialogue am I currently on?
    public int Index = 0;

    void Start()
    {
        //Here I make sure to imprint the first line of dialogue
            //on the text/sprite renderers
        ImprintLine();
    }

    private void Update()
    {
        //If I hit space. . .
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Set the current line of dialogue to the next one
            Index++;
            //And redo all the text and art to match it
            ImprintLine();
        }
    }

    //Makes all the text and art match the dialogue line we're currently on
    public void ImprintLine()
    {
        //If we've hit the end of the script. . .
        if (Index >= Lines.Count)
        {
            //End the game
            SceneManager.LoadScene("You Win");
            return;
        }

        //Find which line of dialogue we're currently on
        DialogueLine current = Lines[Index];
        //Set the text to match that line's dialogue text
        Text.text = current.Text;
        //Find the character art the line of dialogue requests
        Character.sprite = GetCharacter(current.Character);
        //Find the background art the line of dialogue requests
        Background.sprite = GetBackground(current.Background);
    }

    //Convert the text description of a character to a sprite
    public Sprite GetCharacter(string who)
    {
        //If the dialogue line calls for "Gary", use this sprite
        if (who == "Gary") return GaryDefault;
        //And so on. . .
        if (who == "Gary Wink") return GaryWink;
        if (who == "Gary Frown") return GaryFrown;
        if (who == "Vampire") return VampDefault;
        if (who == "Vampire Wink") return VampWink;
        //If Character is left blank, just don't change anything
        return Character.sprite;
    }
    
    //Convert the text description of a background to a sprite
    public Sprite GetBackground(string where)
    {
        //If the dialogue line calls for "Outside", use this sprite
        if (where == "Outside") return OutsideBG;
        //If the dialogue line calls for "Inside", use this sprite
        if (where == "Inside") return InsideBG;
        //If Background is left blank, just don't change anything
        return Background.sprite;
    }
    
}

//This line makes a class appear in the Unity Inspector
  //when used as a variable type
[System.Serializable]
public class DialogueLine
{
    //A custom class that just records dialogue, a character, and a background
    //Think of it almost like a Vector3, but for story instead of position
    public string Text;
    public string Character;
    public string Background;
}
