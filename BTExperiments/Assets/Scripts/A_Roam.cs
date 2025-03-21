using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class A_Roam : ActionTask<NavMeshAgent> {

		public float maxRoamRadius = 10f; // Maximum distance of a circle
		private bool _roamWaypointSet = false; //Boolian that I will use to check if agent got close to the waypoint or no

		private float _minimalDistance = 1.5f; //Stop distance

		protected override void OnExecute() {

		if (_roamWaypointSet == false){
			Vector2 randomCircle = Random.insideUnitCircle * maxRoamRadius; //finding random point in a circle
			Vector3 randomPoint = agent.transform.position + new Vector3 (randomCircle.x,0,randomCircle.y);

			agent.SetDestination(randomPoint);
			
			_roamWaypointSet = true;
		}

		if (_roamWaypointSet == true){
			if (agent.remainingDistance <= _minimalDistance) {
				EndAction(true);
			}

		}
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			


		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}