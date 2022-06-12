using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float swipeSpeed;
    public float moveSpeed;
    private Camera cam;
    private Animator anim;
    private bool running;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        cam = Camera.main;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Animation();
        if (Input.GetMouseButton(0))
        {
            Move();

        }
        else
        {
            running = false;
        }
    }
    private void Move()
    {
        running = true;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.transform.localPosition.z;

        Ray ray = cam.ScreenPointToRay(mousePos);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200))
        {
            Vector3 hitVec = hit.point;
            hitVec.y = transform.localPosition.y;
            hitVec.z = transform.localPosition.z;

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, hitVec, Time.deltaTime * swipeSpeed);
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
            if (hitVec != Vector3.zero)
            {

                hitVec.z = hitVec.z + 2.8f;
                Vector3 toRotation = (hitVec - transform.position);
                transform.rotation = Quaternion.LookRotation(toRotation);
            }
        }


    }
    private void Animation()
    {
        anim.SetBool("running", running);
    }
}