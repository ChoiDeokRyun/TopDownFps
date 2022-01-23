using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        LookCursor();
        AnimationControll(MoveVector());
    }

    protected void LookCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hitresult;
        if (Physics.Raycast(ray, out Hitresult))
        {
            Vector3 MousePosition = Hitresult.point;
            MousePosition.y = this.transform.position.y;

            Vector3 LookPos = (MousePosition - transform.position).normalized;

            transform.LookAt(MousePosition);
        }
    }
    protected Vector3 MoveVector()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 pMoveVector = new Vector3(h, 0, v);

        transform.position += new Vector3(h, 0, v) * Time.deltaTime * 8;

        return pMoveVector;
    }
    protected void AnimationControll(Vector3 MoveVec)
    {
        
    }
}