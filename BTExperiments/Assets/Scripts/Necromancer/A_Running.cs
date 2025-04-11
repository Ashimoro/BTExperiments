using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class A_Running : ActionTask<NavMeshAgent> {

		// Size of the field
		private Vector3 _areaMin = new Vector3(-10, 0, -10);
        private Vector3 _areaMax = new Vector3(10, 0, 10);

		// timer
		public float maxRunningTime = 3f;		
		private float _timer;


		protected override void OnExecute() {
			_timer = 0f;

			float x = Random.Range(_areaMin.x, _areaMax.x); // calculating where to go every time script is activationg
			float z = Random.Range(_areaMin.z, _areaMax.z);

			Vector3 newWaypoint = new Vector3(x, 0, z); // fixating Y coordinate, for necromancer not to fly away

			agent.SetDestination(newWaypoint);
		}


		protected override void OnUpdate() {
			
			_timer += Time.deltaTime;

			if (_timer >= maxRunningTime){
				EndAction(true);

			}

		}

		protected override void OnStop() {
			agent.velocity = Vector3.zero; // reseting velocity, for necromancer to slide
			agent.ResetPath();
		}

	}
}