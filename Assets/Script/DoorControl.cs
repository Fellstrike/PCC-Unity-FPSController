using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    Animator doorAnim;

    public bool readyToOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        doorAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && doorAnim.GetBool("character_nearby"))
        {
            doorAnim.SetBool("character_nearby", false);
            readyToOpen = false;
            other.GetComponent<MoveCamera>().canOpen = false;
        }
        else if (readyToOpen)
        {
            other.GetComponent<MoveCamera>().canOpen = false;
        }
    }
}
