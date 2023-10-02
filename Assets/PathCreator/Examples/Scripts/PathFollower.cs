using UnityEngine;

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
        private EnemyModifiers getInAir;
        [SerializeField] Animator anim;

        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }

            getInAir = GetComponentInChildren<EnemyModifiers>();

            speed = settings.GetSpeed;

            anim = GetComponentInChildren<Animator>();
            anim.SetFloat("animSpeed", speed);

            if (GetComponentInChildren<EnemyModifiers>().GetSpeed) speed += speedModifier;
            pathCreator = GameObject.FindGameObjectWithTag("path").GetComponent<PathCreator>();
        }

        void Update()
        {
            if (pathCreator != null && getInAir.GetInAir())
            {
                distanceTravelled += 2 * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }
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
        public void UpdateSpeed(float f)
        {
            if (GetComponentInChildren<EnemyModifiers>().GetSpeed)
            {
                speed = f;
                speed += speedModifier;
                anim.SetFloat("animSpeed", speed);
            }
            else
            {
                speed = f;
                anim.SetFloat("animSpeed", speed);
            }
        }
    }
}