using UnityEngine;

public class StartGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Triggered by: {other.gameObject.name}");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the StartGate!");
            GameEvents.CallRaceStart();
        }
    }
}
