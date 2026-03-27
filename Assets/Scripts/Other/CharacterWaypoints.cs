using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterWaypoints : MonoBehaviour {


    [Serializable]
    public class Waypoint {

        public Transform transform;
        public float idleTime;

    }


    public event EventHandler OnStateChanged;


    [SerializeField] private List<Waypoint> waypointList;
    [SerializeField] private RenderTexture renderTexture;


    public enum State {
        Walking,
        Idle,
    }


    private int waypointIndex;
    private State state;
    private float idleTimer;


    private void Update() {
        switch (state) {
            case State.Walking:
                Waypoint waypoint = waypointList[waypointIndex];
                Vector3 targetPosition = waypoint.transform.position;
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                float moveSpeed = 1.2f;
                transform.position += moveDir * moveSpeed * Time.deltaTime;

                float rotationSpeed = 10f;
                transform.forward = Vector3.Slerp(transform.forward, moveDir, rotationSpeed * Time.deltaTime);

                float reachedTargetDistance = .2f;
                if (Vector3.Distance(transform.position, targetPosition) < reachedTargetDistance) {
                    idleTimer = waypoint.idleTime;
                    state = State.Idle;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.Idle:
                idleTimer -= Time.deltaTime;
                if (idleTimer <= 0f) {
                    waypointIndex = (waypointIndex + 1) % waypointList.Count;
                    state = State.Walking;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
        }
    }

    public State GetState() {
        return state;
    }



}