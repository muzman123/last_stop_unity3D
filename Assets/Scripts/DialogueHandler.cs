using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField] string dialogue;
    [SerializeField] float dialogueTime;
    [SerializeField] TMP_Text dialogueGUI;
    [SerializeField] string tagCheck;
    [SerializeField] bool disableAfterUse;

    // Start is called before the first frame update
    void Start()
    {
        dialogueGUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == tagCheck)
        {
            dialogueGUI.text = dialogue;
            StartCoroutine(ShowDialogue());
        }
    }

    IEnumerator ShowDialogue()
    {
        dialogueGUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(dialogueTime);
        dialogueGUI.gameObject.SetActive(false);
        if (disableAfterUse)
        {
            Destroy(this);
        }
    }

}
