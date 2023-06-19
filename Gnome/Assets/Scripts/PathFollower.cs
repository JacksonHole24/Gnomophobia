using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float currentSpeed = 5;
        float distanceTravelled;

        public float defaultSpeed = 5;

        float targetSpeed;
        float acceleration;
        bool accelerating;

        float leeway = 0.1f;

        void Start() 
        {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        void Update()
        {
            if (pathCreator != null)
            {
                distanceTravelled += currentSpeed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }

            if (accelerating)
            {
                if (currentSpeed > targetSpeed + leeway || currentSpeed < targetSpeed - leeway)
                {
                    if (currentSpeed < targetSpeed)
                    {
                        currentSpeed += acceleration * Time.deltaTime;
                    }
                    else
                    {
                        currentSpeed -= acceleration * Time.deltaTime;
                    }
                }
                else
                {
                    currentSpeed = targetSpeed;
                    accelerating = false;
                }
            }
            
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }

        public void ChangeSpeed(float targetSpeed, float acceleration)
        {
            this.targetSpeed = targetSpeed;
            this.acceleration = acceleration;

            accelerating = true;
        }
    }
}