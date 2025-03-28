using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class Charge : ActionTask {

			// BBParameters:
		public BBParameter<GameObject> playerLocation;
		public BBParameter<GameObject> fireEyes;
		public BBParameter<float> movementSpeed;

			// Everything related to back steps before charge:
		private float _backDistance = 2f;
		private Vector3 _backPosition;
		private bool _backComplete;
		
			// Everything related to player:
		private Vector3 _dashTarget;

			// Misc:
		private float _distanceThreshold = 0.5f;
		
		protected override void OnExecute() {
			
			fireEyes.value.SetActive(true); // Turn on the fiery eyes

			Vector3 rotationDirection = playerLocation.value.transform.position - agent.transform.position; // Set the position where to turn
			rotationDirection.y = 0; // Make the rotation ignore the y coordinate. This is just in case
			agent.transform.rotation = Quaternion.LookRotation(rotationDirection); // Turn the knight towards the player.

			_backComplete = false; // This must be reset for the charge attack to work properly
			_backPosition = agent.transform.position - agent.transform.forward * _backDistance; // The point to which the knight will retreat before the charge attack.

			_dashTarget = playerLocation.value.transform.position; // The last position of the player, which was when the script was activated
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
			if (!_backComplete) {
				agent.transform.position = Vector3.MoveTowards(agent.transform.position, _backPosition, movementSpeed.value / 5 * Time.deltaTime); // Slowly pulling back

					if(Vector3.Distance(agent.transform.position, _backPosition) < _distanceThreshold){ // Check the distance to the charge point
						_backComplete = true;
					} 

			} 

			
			if (playerLocation.value != null && _backComplete == true){
				agent.transform.position = Vector3.MoveTowards(agent.transform.position, _dashTarget, movementSpeed.value * Time.deltaTime); 
				// ^^ Move towards the player's last position ^^

					if(Vector3.Distance(agent.transform.position, _dashTarget) < _distanceThreshold + 0.6){ // Distance check for stopping + 0.6, due to the size of the player model
						EndAction(true);
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