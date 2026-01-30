using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

	public Image characterIcon;
	public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> lines;
	//event call back
	public System.Action<Dialogue> OnDialogueEnded;
	private Dialogue currentDialogue;


    
	public bool isDialogueActive = false;

	public float typingSpeed = 0.2f;

	public Animator animator;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

		lines = new Queue<DialogueLine>();
    }

	public void StartDialogue(Dialogue dialogue)
	{
		currentDialogue = dialogue;

		isDialogueActive = true;
		animator.Play("show");

		lines.Clear();

		foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
		{
			lines.Enqueue(dialogueLine);
		}

		DisplayNextDialogueLine();
	}

	public void DisplayNextDialogueLine()
	{
		if (lines.Count == 0)
		{
			EndDialogue();
			return;
		}

		DialogueLine currentLine = lines.Dequeue();

		characterIcon.sprite = currentLine.character.icon;
		characterName.text = currentLine.character.name;

		StopAllCoroutines();

		StartCoroutine(TypeSentence(currentLine));
	}

	IEnumerator TypeSentence(DialogueLine dialogueLine)
	{
		dialogueArea.text = "";
		foreach (char letter in dialogueLine.line.ToCharArray())
		{
			dialogueArea.text += letter;
			yield return new WaitForSeconds(typingSpeed);
		}
	}

	void EndDialogue()
	{
		isDialogueActive = false;
		animator.Play("hide");

		OnDialogueEnded?.Invoke(currentDialogue);
	}
	
	public void HideDialogue()
	{
		animator.Play("hide");
	}


	public GameObject choicePanel;
	public Button choiceButtonPrefab;

	public void ShowChoices(List<DialogueChoice> choices)
	{
		choicePanel.SetActive(true);

		foreach (Transform child in choicePanel.transform)
			Destroy(child.gameObject);

		foreach (DialogueChoice choice in choices)
		{
			Button btn = Instantiate(choiceButtonPrefab, choicePanel.transform);
			btn.GetComponentInChildren<TextMeshProUGUI>().text = choice.choiceText;

			btn.onClick.AddListener(() =>
			{
				choicePanel.SetActive(false);
				StartDialogue(choice.nextDialogue);
			});
		}
	}

}