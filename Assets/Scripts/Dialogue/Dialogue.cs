using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue")]

public class Dialogue : ScriptableObject
{
    [TextArea(10, 14)] [SerializeField] string dialogue;

    public string GetDialogue()
    {
        return dialogue;
    }
}
