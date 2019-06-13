using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Dictionary<string, Texture> sustThings = new Dictionary<string, Texture>();
    [SerializeField] private Texture[] textures;
    [SerializeField] private float interactionRayLength = 100.0f;
    
    private bool showInteraction = false;
    private bool showInteractionInfo = false;
    private bool interactionShown = false;
    private string interactionName;
    
    // Start is called before the first frame update
    void Start()
    {
        sustThings.Add("Greenhouse", textures[0]);
        sustThings.Add("Solar Panel", textures[1]);
        sustThings.Add("Wind Turbine", textures[2]);
        sustThings.Add("Rainwater Collector", textures[3]);
        sustThings.Add("Recycle Bins", textures[4]);
    }

    // Update is called once per frame
    void Update()
    {
        InteractRaycast();

        if (showInteraction || interactionShown)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!interactionShown)
                {
                    interactionShown = true;
                    showInteractionInfo = true;
                }
                else
                {
                    interactionShown = false;
                    showInteractionInfo = false;
                }
            }
        }
    }

    private void OnGUI()
    {
        var interactionStyle = new GUIStyle();
        interactionStyle.fontSize = 40;
        interactionStyle.normal.textColor = Color.white;
        
        if (showInteraction || interactionShown)
        {
            GUI.Label(new Rect(Screen.width/2, Screen.height*7/8, 50, 50),
                "E",
                interactionStyle);

            if (showInteractionInfo)
            {
                GUI.DrawTexture(
                    new Rect(
                        Screen.width/6,
                        Screen.height/8,
                        Screen.width*4/6,
                        Screen.height*6/8),
                    sustThings[interactionName]);
            }
        }
    }

    void InteractRaycast()
    {
        Vector3 playerPosition = transform.position;
        Vector3 playerForwardDirection = playerCamera.transform.forward;
       
        Ray interactionRay = new Ray(playerPosition, playerForwardDirection);
        RaycastHit rayInteractionHit;

        Vector3 interactionRayEndPoint = playerForwardDirection * interactionRayLength;

        bool hitFound = Physics.Raycast(interactionRay, out rayInteractionHit, interactionRayLength);
        if (hitFound)
        {
            GameObject hitGameObject = rayInteractionHit.transform.gameObject;

            if (hitGameObject.CompareTag("SustainableThings"))
            {
                showInteraction = true;
                interactionName = hitGameObject.name;
                Debug.Log(interactionName);
            }
            else
            {
                showInteraction = false;
            }
        }
    }
}
