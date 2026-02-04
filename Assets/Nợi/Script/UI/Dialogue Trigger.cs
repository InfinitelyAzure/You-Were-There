using System.Collections.Generic;
using UnityEngine;
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

     public bool triggerOnlyOnce = false; 

    private bool hasTriggered = false;
    private void Start()
    {
        DialogueManager.Instance.OnDialogueEnded += HandleDialogueEnd;
    }

    private void OnDestroy()
    {
    if (DialogueManager.Instance != null)
        DialogueManager.Instance.OnDialogueEnded -= HandleDialogueEnd;
            }

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    private void HandleDialogueEnd(Dialogue endedDialogue)
    {
        // Safety check
        if (endedDialogue == null)
        {
            DialogueManager.Instance.HideDialogue();
            return;
        }

        // If this dialogue has choices, show them and KEEP the dialogue visible
        if (endedDialogue.choices != null && endedDialogue.choices.Count > 0)
        {
            DialogueManager.Instance.ShowChoices(endedDialogue.choices);
            return;
        }

        // If there are no choices, now it's safe to hide the dialogue UI
        DialogueManager.Instance.HideDialogue();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            TriggerDialogue();
            if (triggerOnlyOnce)
            {
                hasTriggered = true;
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
    public List<DialogueChoice> choices = new List<DialogueChoice>();
}

[System.Serializable]
public class DialogueChoice
{
    public string choiceText;
    public Dialogue nextDialogue;
}
