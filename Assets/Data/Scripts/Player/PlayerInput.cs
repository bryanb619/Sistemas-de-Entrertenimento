using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private GameHandler                         Handler;


    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            // change bool in close and open input
            Handler._canInteract = true;
        }

    }
    private void OnTriggerExit(Collider exit)
    {
        if (exit.gameObject.tag == "Player")
        {
            Handler._canInteract = false;
        }

    }
    private void Start()
    {
        Handler = FindObjectOfType<GameHandler>();  
    }
  
}
