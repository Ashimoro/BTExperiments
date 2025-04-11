using System.Threading;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions {

	public class A_PlayerFollow : ActionTask<NavMeshAgent> {

		public BBParameter<GameObject> player;


		private float _distanceThreshold = 3f;


		private float _timer = 0f;
		private float _maxTimer = 3f;

		protected override void OnExecute() {
			_timer = 0f;

		}


		protected override void OnUpdate() {
			agent.SetDestination(player.value.transform.position); // moving knight to the player
			
			_timer += Time.deltaTime;
			if (_timer >= _maxTimer){
				EndAction(true);
		}

			if(Vector3.Distance(agent.transform.position, player.value.transform.position) < _distanceThreshold){ // if the knight is close to the player, it will charge
						EndAction(true);
					} 

			}

		protected override void OnStop() {
			
			agent.ResetPath();
		}

	}
}