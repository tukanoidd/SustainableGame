using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform[] objectGame;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InteractRaycast();
    }

    void InteractRaycast()
    {
        Vector3 playerPosition = transform.position;
        Vector3 playerForwardDirection = playerCamera.transform.forward;
       
        Ray interactionRay = new Ray(playerPosition, playerForwardDirection);
        RaycastHit rayInteractionHit;
        float interactionRayLength = 100.0f;

        Vector3 interactionRayEndPoint = playerForwardDirection * interactionRayLength;
        
        Debug.DrawLine(transform.position, interactionRayEndPoint);
        Debug.Log(playerPosition);
        Debug.Log(playerForwardDirection);
        Debug.Log(interactionRayEndPoint);

        bool hitFound = Physics.Raycast(interactionRay, out rayInteractionHit, interactionRayLength);
        if (hitFound)
        {
            GameObject hitGameObject = rayInteractionHit.transform.gameObject;
            string hitFeedback = hitGameObject.name;
            if (hitGameObject.tag != "Collisions")
            {
                Debug.Log(hitFeedback);    
            }
        }
    }
}
