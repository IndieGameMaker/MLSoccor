using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class BallCtrl : MonoBehaviour
{
    public Agent[] players;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("RED_GOAL"))
        {
            //Blue + Reward
            players[0].AddReward(+1.0f);
            //Red - Reward
            players[1].AddReward(-1.0f);
            players[0].EndEpisode();
            players[1].EndEpisode();

            transform.localPosition = new Vector3(0.0f, 3.0f, 0.0f);
            rb.velocity = rb.angularVelocity = Vector3.zero;
        }

        if (coll.gameObject.CompareTag("BLUE_GOAL"))
        {
            //Blue - Reward
            players[0].AddReward(-1.0f);
            //Red + Reward
            players[1].AddReward(+1.0f);
            players[0].EndEpisode();
            players[1].EndEpisode();
            
            transform.localPosition = new Vector3(0.0f, 3.0f, 0.0f);
            rb.velocity = rb.angularVelocity = Vector3.zero;
        }        
    }
}
