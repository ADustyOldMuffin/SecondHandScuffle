using System.Collections;
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
    [SerializeField] Dialogue[][] dialogueList;

    Dialogue currentDialogue;

    bool firstTime = true;


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
        if (firstTime)
        {
            textComponent.text = dialogueList[0][0].GetDialogue();
            firstTime = false;
        }
        UpdateDialogue();
    }

    public void UpdateDialogue()
    {
<<<<<<< Updated upstream
        if(LevelManager.Instance == null || 
           LevelManager.Instance.Player == null || 
           !LevelManager.Instance.Player.TryGetComponent(out PlayerWeapon currentWeapon))
            return;
        
        Dialogue newDialogue = dialogueList[currentWeapon.GetCurrentWeaponIndex()];
=======
        int currentWeapon = LevelManager.Instance.Player.GetComponent<PlayerWeapon>().GetCurrentWeaponIndex();
        Dialogue newDialogue = dialogueList[currentWeapon][Random.Range(0,dialogueList[currentWeapon].Length)];
>>>>>>> Stashed changes

        textComponent.text = newDialogue.GetDialogue();
    }

}
