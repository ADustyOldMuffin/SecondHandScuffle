﻿using System.Collections;
using System.Collections.Generic;
using Constants;
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
        if(LevelManager.Instance == null)
            return;
        
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
        if(LevelManager.Instance == null || 
           LevelManager.Instance.Player == null || 
           !LevelManager.Instance.Player.TryGetComponent(out PlayerWeapon currentWeapon))
            return;
        
        Dialogue newDialogue = dialogueList[currentWeapon.GetCurrentWeaponIndex()];

        textComponent.text = newDialogue.GetDialogue();
    }

}
