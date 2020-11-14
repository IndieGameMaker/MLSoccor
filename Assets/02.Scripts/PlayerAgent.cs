using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Policies;

public class PlayerAgent : Agent
{
    public enum TEAM
    {
        BLUE = 0, RED = 1
    }

    public TEAM team;
    public GameObject[] players;

    //플레이어의 초기 위치값
    private Vector3 initPos;
    //Behaviour Parameters Team ID 추출
    private BehaviorParameters bps;
    private Rigidbody rb;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        bps = GetComponent<BehaviorParameters>();

        rb.maxAngularVelocity = 500.0f;
        //Team ID 추출
        team = (TEAM)bps.TeamId;

        //Team ID 별 초기설정
        if (team == TEAM.BLUE)
        {
            players[bps.TeamId].SetActive(true);
            initPos = new Vector3(-6.0f, 0.5f, 0.0f);
            transform.localPosition = initPos;
            transform.localRotation = Quaternion.Euler(Vector3.up * 90.0f);
        }

        if (team == TEAM.RED)
        {
            players[bps.TeamId].SetActive(true);
            initPos = new Vector3(+6.0f, 0.5f, 0.0f);
            transform.localPosition = initPos;
            transform.localRotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
        }
    }

    public override void OnEpisodeBegin()
    {

    }

    public override void CollectObservations(VectorSensor sensor)
    {

    }

    public override void OnActionReceived(float[] vectorAction)
    {

    }

    public override void Heuristic(float[] actionsOut)
    {

    }
}
