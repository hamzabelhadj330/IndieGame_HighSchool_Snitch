using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteractor : MonoBehaviour
{
    [Header("Raycast Features")]
    [SerializeField] private float rayDistance = 5;

    [Header("Raycast Features")]
    [SerializeField] private KeyCode interactionKey;
    [SerializeField] private Image crosshair;

    private DoorItem doorItem;
    private Camera _camera;




    void Start()
    {
        _camera = GetComponent<Camera>();
    }

   
    void Update()
    {
        PerformRaycast();
        InteractionInput();
    }

    void PerformRaycast()
    {
        if (Physics.Raycast(_camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f)), transform.forward, out RaycastHit hit, rayDistance))
        {
            var _doorItem = hit.collider.GetComponent<DoorItem>();
            if (_doorItem != null)
            {
                doorItem = _doorItem;
                HighlightCrosshair(true);
            }
            else
            {
                ClearItem();
            }
        }
        else
        {
            ClearItem();
        }
    }

    void InteractionInput()
    {
        if (doorItem != null)
        {
            if (Input.GetKeyDown(interactionKey))
            {
                doorItem.ObjectInteraction();
            }
        }
    }

    void ClearItem()
    {
        if (doorItem != null)
        {
            doorItem = null;
            HighlightCrosshair(false);
        }
    }

    void HighlightCrosshair(bool on)
    {
        crosshair.color = on ? Color.red : Color.white;
    }

}
