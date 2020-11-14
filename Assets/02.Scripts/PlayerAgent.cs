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
