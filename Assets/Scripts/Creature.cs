using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZiahTowerDefense
{
    /// <summary>
    /// What all enemies and NPCs will inherit from. 
    /// </summary>
    public abstract class Creature : MonoBehaviour, ILifeBased
    {
        /// <summary>
        /// Creatures max life.
        /// </summary>
        private float maxLife;

        /// <summary>
        /// Creatures current life.
        /// </summary>
        private float currentLife;

        /// <summary>
        /// How fast the creature moves.
        /// </summary>
        private float movingSpeed = 10;

        /// <summary>
        /// Creatures title/name.
        /// </summary>
        private string title;

        /// <summary>
        /// If the creature can move. Better to use a state pattern design.
        /// </summary>
        private bool ableToMove = false;

        /// <summary>
        /// Start Position where creatures are moving from
        /// </summary>
        private Vector3 startPosition;

        /// <summary>
        /// Position the creature is moving to.
        /// </summary>
        private Vector3 endPosition;

        /// <summary>
        /// The time the creature starts moving.
        /// </summary>
        private float startTime;

        /// <summary>
        /// How long it is between start and end position
        /// </summary>
        private float journeyLength;

        /// <summary>
        /// The distance to the point in the path that accepted a complete movement. 
        /// </summary>
        private float distanceToPoint = 0.013f;

        /// <summary>
        /// Where the creature is in the path to follow.
        /// </summary>
        private int currentPathPoint = 0;

        /// <summary>
        /// Path the creature follows.
        /// </summary>
        private List<Vector3> pathToFollow = new List<Vector3>();

        /// <summary>
        /// Sets and gets creatures movingSpeed;
        /// </summary>
        public float MovingSpeed { get => movingSpeed; set => movingSpeed = value; }

        /// <summary>
        /// Sets and gets creatures title;
        /// </summary>
        public string Title { get => title; set => title = value; }

        /// <summary>
        /// Gets and sets ableToMove.
        /// </summary>
        public bool AbleToMove { get => ableToMove; set => ableToMove = value; }

        float ILifeBased.MaxLife { get => maxLife; set => maxLife = value; }

        float ILifeBased.CurrentLife { get => currentLife; set => currentLife = value; }
        
        /// <summary>
        /// Handles when the creature dies.
        /// </summary>
        public virtual void DoneDidDied()
        {
            Destroy(gameObject);//A better way is to play a script that handles a unique death. 
        }

        /// <summary>
        /// Handles when a creature reaches the end of the road.
        /// </summary>
        public virtual void ReachedTheEnd()
        {
            DoneDidDied();//Can overwrite this for other creatures.
        }

        /// <summary>
        /// Sets up the path for the createure to follow
        /// </summary>
        /// <param name="newPath"></param>
        public void SutUpPathway(List<Vector3> newPath) {
            ableToMove = true;
            pathToFollow = newPath;
            currentPathPoint = 0;
        } 

        /// <summary>
        /// Calculates the next movement for the creature.
        /// </summary>
        public virtual void CalculateMovement()
        {
            float distance = Vector3.Distance(transform.position, endPosition);

            if (distance < distanceToPoint) {
                currentPathPoint++;
            }

            if (currentPathPoint >= pathToFollow.Count)
            {
                ableToMove = false;
                ReachedTheEnd();
            }
            else {
                if (distance < distanceToPoint)
                {
                    SetUpNextPoit(pathToFollow[currentPathPoint]);
                }
                else {
                    MoveToEndMarker();
                }
            }
        }

        /// <summary>
        /// Sets up a creature to move to the next point.
        /// </summary>
        /// <param name="newEndPosition">The position to move to</param>
        public void SetUpNextPoit(Vector3 newEndPosition)
        {
            startTime = Time.time;
            startPosition = transform.position;
            endPosition = new Vector3(newEndPosition.x, transform.position.y, newEndPosition.z);

            journeyLength = Vector3.Distance(startPosition, endPosition);

            
        }


        /// <summary>
        /// Moved the creature to endTarget.
        /// </summary>
        private void MoveToEndMarker()
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * movingSpeed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
            //Looks at target but keeps from looking up or down.
            transform.LookAt(endPosition);
        }

        public void ChangeHealth(float amount)
        {
            currentLife += amount;
            if (currentLife < 0)
            {
                DoneDidDied();
            }
        }
    }
}
