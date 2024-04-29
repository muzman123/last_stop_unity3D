using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InstructionGUI : MonoBehaviour
{
    [SerializeField] string instruction;
    [SerializeField] TMP_Text instructionText;
    [SerializeField] string tagCheck;

    bool enteredTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        instructionText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(enteredTrigger && Input.GetKeyDown(KeyCode.E))
        {
            instructionText.gameObject.SetActive(false);
            Destroy(this);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            instructionText.text = instruction;
            instructionText.gameObject.SetActive(true);
            enteredTrigger = true;
        }
    }

    void OnTriggerExit(Collider other) 
    {
        instructionText.gameObject.SetActive(false);

    }
}
