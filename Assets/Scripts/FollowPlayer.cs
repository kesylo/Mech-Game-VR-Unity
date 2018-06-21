using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour {

    Transform player;
    NavMeshAgent agent;

    // Update is called once per frame
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update ()
    {
            agent.SetDestination(player.position);
	}
}
