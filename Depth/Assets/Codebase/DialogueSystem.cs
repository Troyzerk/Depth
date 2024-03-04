using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;
    public List<string> dialogueLines = new();
    public string npcName;

    //Grabbing UI elements
    public GameObject dialogueBox;
    Button continueButton;
    TMP_Text dialogueText, nameText;
    int dialogueIndex;

    private void Init()
    {
        continueButton = dialogueBox.transform.Find("NextButton").GetComponent<Button>();
        dialogueText = dialogueBox.transform.Find("Content/DialogueText").GetComponent<TMP_Text>();
        nameText = dialogueBox.transform.Find("SpeakerName").GetComponent<TMP_Text>();

        continueButton.onClick.AddListener(delegate { ContinueDialogue(); });
        dialogueBox.SetActive(false);
        
        if(instance!=null && instance!= this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void AddNewDialogue(string[] lines, string npcName)
    {
        dialogueIndex = 0;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);
        this.npcName = npcName;
        CreateDialogue();
    }

    public void CreateDialogue()
    {
        dialogueText.text = dialogueLines[dialogueIndex];
        nameText.text = npcName;
        dialogueBox.SetActive(true);
    }

    public void ContinueDialogue()
    {
        if (dialogueIndex < dialogueLines.Count -1)
        {
            dialogueIndex++;
            dialogueText.text = dialogueLines[dialogueIndex];
        }
        else
        {
            dialogueBox.SetActive(false);
        }
    }
}
