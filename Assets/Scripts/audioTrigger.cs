using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioTrigger : MonoBehaviour
{
    public AudioSource playsound;
    [SerializeField] AudioClip afterKnock;
    bool active = false;

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.E) && active)
        {
            playsound.Play(); 
            StartCoroutine(AfterKnock());
        }   
      
    }
    void OnTriggerEnter(Collider other) 
    {
        active = true;
    }

    void OnTriggerExit(Collider other)
    {
        active = false;
    }

    IEnumerator AfterKnock()
    {
        yield return new WaitForSeconds(2f);
        playsound.PlayOneShot(afterKnock, 0.5f);
    }
}
