using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject hook;
    public GameObject hookHolder;

    [Range(0, 100)] public float hookTravelSpeed;
    [Range(0, 100)] public float playerTravelSpeed;
    [Range(0, 100)] public float climbingUpDistance; //Used to push the player upwards on to a platform
    [Range(0, 100)] public float climbingForwardDistance; //Used to push the player forwards to stand on a platform

    public static bool fired;
    public bool hooked;
    public GameObject hookedObject;

    [Range(0, 100)] public float maxDistance;
    [SerializeField] private float currentDistance;


    private bool grounded;

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        //Firing the grappling hook
        if(Input.GetKeyDown(playerController.grapplingHookKey) && fired == false)        
            fired = true;

        if(fired)
        {
            LineRenderer rope = hook.GetComponent<LineRenderer>();
            rope.SetVertexCount(2);
            rope.SetPosition(0, hookHolder.transform.position);
            rope.SetPosition(1, hook.transform.position);
        }

        if (fired == true && hooked == false)
        {
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);

            if (currentDistance >= maxDistance)
                ReturnHook();
        }

        if (hooked == true && fired == true)
        {
            hook.transform.parent = hookedObject.transform;
            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * playerTravelSpeed);
            float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);

            this.GetComponent<Rigidbody>().useGravity = false;

            if (distanceToHook < 1)
            {
                if (grounded == false)
                {
                    this.transform.Translate(Vector3.forward * Time.deltaTime * climbingForwardDistance);
                    this.transform.Translate(Vector3.up * Time.deltaTime * climbingUpDistance);
                }

                StartCoroutine("Climb");
            }
        }
        else
        {
            hook.transform.parent = hookHolder.transform;
            this.GetComponent<Rigidbody>().useGravity = true;
        }

    }

    IEnumerator Climb()
    {
        yield return new WaitForSeconds(0.1f);
        ReturnHook();
    }

    void ReturnHook()
    {
        hook.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;

        LineRenderer rope = hook.GetComponent<LineRenderer>();
        rope.SetVertexCount(0);
    }

    void CheckIfGrounded()
    {
        RaycastHit hit;
        float distance = 1.0f;
        Vector3 dir = new Vector3(0, -1);

        if(Physics.Raycast(transform.position, dir, out hit, distance))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

}
