using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomBoss : MonoBehaviour
{
    [SerializeField] private GameObject boss, gateRoomBoss, healthBarBoss;
    [SerializeField] private GameManagerScript managerScript;
    void Update()
    {
        if (boss == null)
        {
            Destroy(healthBarBoss);
            Destroy(gateRoomBoss);
            managerScript.Victory();
        }
    }
    public void OnTriggerStay2D()
    {
        gateRoomBoss.SetActive(true);
        healthBarBoss.SetActive(true);
        boss.SetActive(true);
    }
}
