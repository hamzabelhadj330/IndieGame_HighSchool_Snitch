using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Camera : MonoBehaviour
{
    //The distance from which the player can interact with an object
    public float interactionDistance;

    //Text or crosshair that shows up to let the player know they can interact with an object they're looking at
    public GameObject interactionText;

    //Layers the raycast can hit/interact with. Any layers unchecked will be ignored by the raycast.
    public LayerMask interactionLayers;

    //The Update() void is used to make stuff happen every frame
    void Update()
    {
        //RaycastHit variable which will collect information from objects the raycast hits
        RaycastHit hit;

        //If the raycast hits something,
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, interactionLayers))
        {
            //If the object it hits is named next cam button,
            if (hit.collider.gameObject.name == "next cam button")
            {
                //The interaction text will enable
                interactionText.SetActive(true);

                //If the E key is pressed,
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //The component is accessed and the function is performed
                    hit.collider.gameObject.GetComponent<Security_Cam_System>().nextCam();
                }
            }
            //Else if the object it hits is named previous cam button,
            else if (hit.collider.gameObject.name == "previous cam button")
            {
                //The interaction text will enable
                interactionText.SetActive(true);

                //If the E key is pressed,
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //The component is accessed and the function is performed
                    hit.collider.gameObject.GetComponent<Security_Cam_System>().previousCam();
                }
            }
            //else, the interaction text is set false.
            else
            {
                interactionText.SetActive(false);
            }
        }
        //else, the interaction text is set false.
        else
        {
            interactionText.SetActive(false);
        }
    }
}
