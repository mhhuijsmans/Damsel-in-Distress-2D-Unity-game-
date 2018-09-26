using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour {

	[HideInInspector]
	public GameObject[] waypoints;
	private int currentWaypoint = 0;
	private float lastWaypointSwitchTime;
	public float pathLength;
	public float totalTimeForPath;
	public float currentTimeOnPath;
	public float currentTimeOnPathPlusDelay;
	public float delayTime;
	public float speed = 1f;

	void Start() {
		lastWaypointSwitchTime = Time.time;
		delayTime = 0f;
	}
    
    void Update() {
        
        Vector3 startPosition = waypoints [currentWaypoint].transform.position;
        Vector3 endPosition = waypoints [currentWaypoint + 1].transform.position;
        
        pathLength = Vector3.Distance (startPosition, endPosition);
		totalTimeForPath = pathLength / speed;
		currentTimeOnPath = Time.time - lastWaypointSwitchTime;
		currentTimeOnPathPlusDelay = (Time.time - lastWaypointSwitchTime) - delayTime;
		gameObject.transform.position = Vector2.Lerp (startPosition, endPosition, currentTimeOnPathPlusDelay / totalTimeForPath);
        
        if (gameObject.transform.position.Equals(endPosition)) {
          if (currentWaypoint < waypoints.Length - 2) {
            currentWaypoint++;
            lastWaypointSwitchTime = Time.time;
          }
            
          else {
            Destroy(gameObject);

				GameManagerBehaviour gameManager =
					GameObject.Find("GameManager").GetComponent<GameManagerBehaviour>();
				gameManager.Health -= 1;
          }
        }
    }

	public float DistanceToGoal() {
		float distance = 0;
		distance += Vector2.Distance (
			gameObject.transform.position,
			waypoints [currentWaypoint + 1].transform.position);
		for (int i = currentWaypoint + 1; i < waypoints.Length - 1; i++) {
			Vector3 startPosition = waypoints [i].transform.position;
			Vector3 endPosition = waypoints [i + 1].transform.position;
			distance += Vector2.Distance (startPosition, endPosition);
		}
		return distance;
	}
}
