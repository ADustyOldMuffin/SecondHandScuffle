using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{


    [SerializeField] TextMeshProUGUI textComponent;
    [SerializeField] Dialogue startingDialogue;
    [SerializeField] Dialogue[] dialogueList;

    Dialogue currentDialogue;


    // Start is called before the first frame update
    void Awake()
    {
        currentDialogue = startingDialogue;
        textComponent.text = currentDialogue.GetDialogue();

    }

    public void UpdateDialogue(int index)
    {
        Dialogue newDialogue = dialogueList[index];

        textComponent.text = newDialogue.GetDialogue();
    }

}
