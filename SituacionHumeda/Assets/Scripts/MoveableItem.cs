using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableItem : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 mPos;
    private bool held = false;
    // Start is called before the first frame update
    void Start()
    {
        startPos = this.transform.localPosition;
        mPos = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        if (held == true)
        { 
            mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mPos - startPos;
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
    }

    public void OnMouseDown()
    {

        if (Input.GetMouseButtonDown(0))
        {
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
            startPos.z = 0;
            held = true;
        }
        
    }

    public void OnMouseUp()
    {
        held = false;
    }
}
