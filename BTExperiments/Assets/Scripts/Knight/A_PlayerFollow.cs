using System.Threading;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions {

	public class A_PlayerFollow : ActionTask<NavMeshAgent> {

		public BBParameter<GameObject> player;


		private float _distanceThreshold = 1.0f;
		private float _timer = 0f;
		private float _maxTimer = 3f;


		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
		
			_timer = 0f;

		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
			agent.SetDestination(player.value.transform.position);
			
			_timer += Time.deltaTime;
			if (_timer >= _maxTimer){
				EndAction(true);
			}

		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
			agent.ResetPath();
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}