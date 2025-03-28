using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class Charge : ActionTask {

		
		public BBParameter<GameObject> playerLocation;
		public BBParameter<GameObject> fireEyes;
		public BBParameter<float> movementSpeed;

		private float _backDistance = 2f;
		private Vector3 _backPosition;
		private bool _backComplete;


		private float _distanceThreshold = 0.1f;

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			
			fireEyes.value.SetActive(true);

			Vector3 rotationDirection = playerLocation.value.transform.position - agent.transform.position;
			rotationDirection.y = 0;
			agent.transform.rotation = Quaternion.LookRotation(rotationDirection);

			_backComplete = false;
			_backPosition = agent.transform.position - agent.transform.forward * _backDistance;

		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
			if (!_backComplete) {
				agent.transform.position = Vector3.MoveTowards(agent.transform.position, _backPosition, movementSpeed.value / 2 * Time.deltaTime);

					if(Vector3.Distance(agent.transform.position, _backPosition) < _distanceThreshold){
						_backComplete = true;
					}

			if (playerLocation.value != null && _backComplete == true){
				Vector3 _dashTarget = playerLocation.value.transform.position;

				agent.transform.position = Vector3.MoveTowards(agent.transform.position, _dashTarget, movementSpeed.value * Time.deltaTime);

					if(Vector3.Distance(agent.transform.position, _dashTarget) < _distanceThreshold){
						EndAction(true);
					}
			}

			} 
			


		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
			fireEyes.value.SetActive(false);

		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}