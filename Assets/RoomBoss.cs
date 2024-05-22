using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomBoss : MonoBehaviour
{
    [SerializeField] private GameObject boss, gateRoomBoss, healthBarBoss;
    private void OnDisable()
    {
        gateRoomBoss.SetActive(true);
        healthBarBoss.SetActive(true);
        boss.SetActive(true);
    }
    public void OnTriggerStay2D()
    {
        gateRoomBoss.SetActive(true);
        healthBarBoss.SetActive(true);
        boss.SetActive(true);
    }
}
