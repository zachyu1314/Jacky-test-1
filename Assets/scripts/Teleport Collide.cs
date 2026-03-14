using UnityEngine;

public class TeleportCollide : MonoBehaviour
{
    public GameObject respawnPoint;
    public GameObject Player;
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
            CharacterController cc = Player.GetComponent<CharacterController>();
            cc.enabled = false;
            Player.transform.position = respawnPoint.transform.position;
            cc.enabled = true;
        }
    }
}
