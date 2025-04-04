using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class A_Running : ActionTask<NavMeshAgent> {

		public float maxRunningTime = 3f;		

		private Vector3 _areaMin = new Vector3(-10, 0, -10);
        private Vector3 _areaMax = new Vector3(10, 0, 10);
		private float _timer;

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			_timer = 0f;

			float x = Random.Range(_areaMin.x, _areaMax.x);
			float z = Random.Range(_areaMin.z, _areaMax.z);

			Vector3 newWaypoint = new Vector3(x, 0, z);

			agent.SetDestination(newWaypoint);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
			_timer += Time.deltaTime;

			if (_timer >= maxRunningTime){
				EndAction(true);

			}

		}

		//Called when the task is disabled.
		protected override void OnStop() {
			agent.velocity = Vector3.zero;
			agent.ResetPath();
		}

	}
}