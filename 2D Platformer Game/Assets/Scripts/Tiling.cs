using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tiling : MonoBehaviour
{
    public int offsetX = 2;
    public bool hasLeftBuddy = false;
    public bool hasRightBuddy = false;

    public bool revereScale = false;

    private float spriteWidth = 0f;

    private Camera cam;

    private Transform myTransform;


    void Awake()
    {
       cam = Camera.main;
        myTransform = transform;
    }


    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasLeftBuddy==false || hasRightBuddy==false)
        {
            float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;

            float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;
            if (cam.transform.position.x + offsetX >= edgeVisiblePositionRight && hasRightBuddy==false)
            {
                MakeNewBuddy(1);
                hasRightBuddy = true;
            }
            else if(cam.transform.position.x-offsetX<= edgeVisiblePositionLeft && hasLeftBuddy == false)
            {
                MakeNewBuddy(-1);
                hasLeftBuddy = true;
            }
        }
        
    }

    void MakeNewBuddy(int rightOrLeft)
    {
        Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth, myTransform.position.y, myTransform.position.z);
        Transform newBuddy = Instantiate(myTransform, newPosition, myTransform.rotation) as Transform;

        if(revereScale==true) //If the object is not tilable.
        {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
        }
        newBuddy.parent = myTransform.parent; //Parenting the new instantiated gameobject
        if(rightOrLeft>0)
        {
            newBuddy.GetComponent<Tiling>().hasLeftBuddy = true;
        }
        else
        {
            newBuddy.GetComponent<Tiling>().hasRightBuddy = true;
        }
    }
}
