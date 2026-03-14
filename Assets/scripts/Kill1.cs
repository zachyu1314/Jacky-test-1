using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Timers;
using UnityEngine;

public class Kill1 : MonoBehaviour
{
    public GameObject respawnPoint;
    public GameObject Player;
    public float cooldown = 2.0f;
    private float nextTeleportTime = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Touched player.");
            if (Time.time >= nextTeleportTime)
            {
                CharacterController cc = Player.GetComponent<CharacterController>();
                cc.enabled = false;
                Player.transform.position = respawnPoint.transform.position + Vector3.forward * 2;
                cc.enabled = true;
                nextTeleportTime = Time.time + cooldown;
            }
        }
    }
}
