﻿using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        [SerializeField] public EnemySettings settings;
        public float speed;
        [SerializeField] private float speedModifier;
        [SerializeField] private float distanceTravelled;
        [SerializeField] private EnemyModifiers slimeStatus;

        [SerializeField] private float slimeHeight = 0.015f;
        [SerializeField] Animator anim;

        
        void Start() {
            // if (pathCreator != null)
            // {
            //     // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            //     pathCreator.pathUpdated += OnPathChanged;
            // }
            slimeStatus = GetComponentInChildren<EnemyModifiers>();

            speed = settings.GetSpeed;

            anim = GetComponentInChildren<Animator>();
            anim.SetFloat("animSpeed", speed);

            pathCreator = GameObject.FindGameObjectWithTag("path").GetComponent<PathCreator>();
  
        }

        void Update()
        {
            if (pathCreator != null && (slimeStatus.GetInAir() || slimeStatus.GetFlying))
            {
                MoveAlongPath();
            }
        }

        void MoveAlongPath() {
            distanceTravelled += speed * Time.deltaTime;
            Vector3 pathPosition = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.position = new Vector3(pathPosition.x, slimeHeight, pathPosition.z);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }

        public float GetDistanceTravelled
        {
            get { return distanceTravelled; }
            set { distanceTravelled = value; }
        }

        public void UpdateModifier(EnemyModifierSettings mod) {
            if (mod.Flying) { 
                slimeHeight = 1.75f;
            }
            if (mod.Speed) {
                UpdateSpeed(speedModifier);
            }
        }
        public void UpdateSpeed(float f) {
            speed = f;
            anim.SetFloat("animSpeed", speed);
        }

    }
}