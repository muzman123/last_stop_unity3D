using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGUI : MonoBehaviour
{
    [SerializeField] GameObject notePic;
    bool isNearNote = false;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        notePic.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isNearNote == true && Input.GetKeyDown(KeyCode.E))
        {
            notePic.SetActive(true);
        }
        else if(isNearNote == false)
        {
            notePic.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            player = other.gameObject;
            isNearNote = true;
        }
    }

    private void OnTriggerExit(Collider other) {

        if(other.tag == "Player")
        {
            isNearNote = false;
        }
    }
}
