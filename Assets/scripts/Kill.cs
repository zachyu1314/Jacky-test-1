using UnityEngine;

public class Kill : MonoBehaviour
{
    public GameObject respawnPoint;
    public GameObject Player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Die();
        }
    }

    public void Die()
    {
        if (Player != null && respawnPoint != null)
        {
            CharacterController cc = Player.GetComponent<CharacterController>();

            if (cc != null) cc.enabled = false;

            Player.transform.position = respawnPoint.transform.position;
            Player.transform.rotation = respawnPoint.transform.rotation;

            if (cc != null) cc.enabled = true;
        }
    }
}