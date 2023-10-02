using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelCollision : MonoBehaviour
{
    [SerializeField] private GameObject PanelUI, player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        PanelUI.SetActive(false);
    }

    void Update()
    {
        transform.LookAt(player.transform);
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
    }

    private void OnTriggerStay(Collider other)
    {
        if (PanelUI.activeSelf) return; // If the UI is already active return

        // If the left hand or right hand enter the trigger turn the UI on
        if (other.CompareTag("player"))
        {
            PanelUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (PanelUI.activeSelf) // If the UI is active
        {
            // If left hand or right hand leave the area set UI to false
            if (other.CompareTag("player"))
                PanelUI.SetActive(false);
        }
    }
}
