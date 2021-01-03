using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Managers;
using Player;

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
        LevelManager.OnPlayerScoreChange += UpdateDialogue;
    }

    void Start()
    {
        UpdateDialogue();
    }

    public void UpdateDialogue()
    {
        int currentWeapon = LevelManager.Instance.Player.GetComponent<PlayerWeapon>().GetCurrentWeaponIndex();
        Dialogue newDialogue = dialogueList[currentWeapon];

        textComponent.text = newDialogue.GetDialogue();
    }

}
