using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingWayPoint : Waypoint
{
    [Range(0f, 100f)]
    [SerializeField] private float talkingTime = 1.2f;
    [SerializeField] Transform lookAtTarget;
    public override void waypointReached(NPCMovementManager npcMover)
    {
        StartCoroutine(Talking(npcMover));
    }

    IEnumerator Talking(NPCMovementManager npcMover)
    {


        Animator animator = npcMover.GetComponent<Animator>();
        if (animator != null)
        {
            //TODO make smooth transition instead of sudden movement
            Vector3 lookTarget = new Vector3(lookAtTarget.position.x, transform.position.y, lookAtTarget.position.z);
            npcMover.transform.LookAt(lookTarget, Vector3.up);
            animator.SetBool("Talking", true);

            yield return new WaitForSeconds(talkingTime);

            animator.SetBool("Talking", false);



        }

        base.waypointReached(npcMover);
    }

}
