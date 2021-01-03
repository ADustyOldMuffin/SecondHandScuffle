using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectDialogue : MonoBehaviour
{

    [SerializeField] Dialogue[] dialogueList;

    public Dialogue RandomDialogue()
    {
        return dialogueList[Random.Range(0, dialogueList.Length)];
    }

    public Dialogue GetSpecificDialogue(int index)
    {
        return dialogueList[index];
    }
}
