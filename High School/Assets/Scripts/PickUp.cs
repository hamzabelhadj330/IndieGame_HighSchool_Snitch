using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    bool isHolding=false;

    [SerializeField] float throwForce = 600f;
    [SerializeField] float maxDistance = 3f;

    float distance;

    TempParent tempParent;

    Rigidbody rb;
    Vector3 objectPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tempParent = TempParent.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(isHolding)
        {
            Hold();
        }
    }

    private void OnMouseDown()
    {
        //pickup
        if (tempParent != null)
        {

            distance = Vector3.Distance(this.transform.position, tempParent.transform.position);

            if (distance <= maxDistance)
            {
                isHolding = true;
                rb.useGravity = false;
                rb.detectCollisions = true;

                this.transform.SetParent(tempParent.transform);
            }
        }
        else
        {
            Debug.Log("temp parent item not found in scene");
        }
    }

    private void OnMouseUp()
    {
        Drop();
    }

    private void OnMouseExit()
    {
        Drop();
    }

    private void Hold()
    {
        distance = Vector3.Distance(this.transform.position, tempParent.transform.position);

        if(distance >= maxDistance)
        {
            Drop();
        }

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        if(Input.GetMouseButtonDown(1))
        {
            //throw
            rb.AddForce(tempParent.transform.forward * throwForce);
            Drop();

        }
    }

    private void Drop()
    {
        if(isHolding)
        {
            isHolding=false;
            objectPos = this.transform.position;
            this.transform.position = objectPos;
            this.transform.SetParent(null);
            rb.useGravity=true;
        }
    }

}
