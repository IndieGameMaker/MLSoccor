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
        InitPlayers();
    }

    public void InitPlayers()
    {
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
        rb.velocity = rb.angularVelocity = Vector3.zero;
    }

    public override void OnEpisodeBegin()
    {
        InitPlayers();
    }

    public override void CollectObservations(VectorSensor sensor)
    {

    }

    public override void OnActionReceived(float[] vectorAction)
    {
        //Debug.Log($"[0]={vectorAction[0]} , [1]={vectorAction[1]} , [2]={vectorAction[2]} ");
        Vector3 dir = Vector3.zero;
        Vector3 rot = Vector3.zero;

        int forward = (int)vectorAction[0];
        int right   = (int)vectorAction[1];
        int rotate  = (int)vectorAction[2];

        switch(forward)
        {
            case 0: break;
            case 1:
                    dir = transform.forward;
                    break;
            case 2: 
                    dir = transform.forward * -1f;
                    break;
        }
        switch(right)
        {
            case 0: break;
            case 1:
                    dir = transform.right * -1f;    //왼쪽으로 이동
                    break;
            case 2:
                    dir = transform.right;          //오른쪽으로 이동
                    break;
        }
        switch(rotate)
        {
            case 0: break;
            case 1:
                    rot = transform.up * -1f;   //왼쪽으로 회전
                    break;
            case 2: 
                    rot = transform.up;         //오른쪽으로 회전
                    break;
        }

        //회전 및 이동처리
        transform.Rotate(rot, Time.deltaTime * 100.0f);
        rb.AddForce(dir * moveSpeed, ForceMode.VelocityChange);
    }

    public float moveSpeed = 1.0f;

    public override void Heuristic(float[] actionsOut)
    {
        //배열의 초기화
        System.Array.Clear(actionsOut, 0, actionsOut.Length);

        //전후진 이동 0, 1, 2 (Non, W, S) Branch[0] size = 3
        if (Input.GetKey(KeyCode.W))
        {
            actionsOut[0] = 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            actionsOut[0] = 2f;
        }

        //좌우 이동 0, 1, 2 (Non, Q, E) Branch[1] size = 3
        if (Input.GetKey(KeyCode.Q))
        {
            actionsOut[1] = 1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            actionsOut[1] = 2f;
        }

        //좌우 회전 0, 1, 2 (Non, A, D) Branch[2] size = 3
        if (Input.GetKey(KeyCode.A)) actionsOut[2] = 1f;
        if (Input.GetKey(KeyCode.D)) actionsOut[2] = 2f;

    }
}
