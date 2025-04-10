using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class A_Charge : ActionTask <NavMeshAgent> {

			// BBParameters:
		public BBParameter<GameObject> playerLocation;
		public BBParameter<GameObject> fireEyes;
		public BBParameter<GameObject> normalEyes;
		public BBParameter<float> movementSpeed;

			// Everything related to back steps before charge:
		private float _backDistance = 2f;
		private Vector3 _backPosition;
		private bool _backComplete;
		
			// Everything related to player:
		private Vector3 _dashTarget;

			// Misc:
		private float _distanceThreshold = 0.5f;
		private float _groundY;

		private float _timer = 0f;
		private float _maxTimer = 3f;
		
		protected override void OnExecute() {

			_timer = 0f;
			
			agent.velocity = Vector3.zero;

			fireEyes.value.SetActive(true); // Turn on the fiery eyes
			normalEyes.value.SetActive(false);

			_groundY = agent.transform.position.y; //Y position fixation (to not go uderground)

			Vector3 rotationDIrection = playerLocation.value.transform.position - agent.transform.position; // Set the position where to turn
			rotationDIrection.y = 0; // Make the rotation ignore the y coordinate. This is just in case
			agent.transform.rotation = Quaternion.LookRotation(rotationDIrection); // Turn the knight towards the player.

			_backComplete = false; // This must be reset for the charge attack to work properly
			_backPosition = agent.transform.position - agent.transform.forward * _backDistance; // The point to which the knight will retreat before the charge attack.

			_dashTarget = playerLocation.value.transform.position; // The last position of the player, which was when the script was activated
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {

			if (!_backComplete) {
				Vector3 newPosition = Vector3.MoveTowards(agent.transform.position, _backPosition, movementSpeed.value / 5 * Time.deltaTime); // Slowly pulling back

				agent.transform.position = new Vector3(newPosition.x, _groundY, newPosition.z);

					if(Vector3.Distance(agent.transform.position, _backPosition) < _distanceThreshold){ // Check the distance to the charge point
						_backComplete = true;
					} 

					_timer += Time.deltaTime;
					if (_timer >= _maxTimer){
					EndAction(true);
			}

			} 

			
			if (playerLocation.value != null && _backComplete == true){
				Vector3 newPosition = Vector3.MoveTowards(agent.transform.position, _dashTarget, movementSpeed.value * Time.deltaTime); 
				agent.transform.position = new Vector3(newPosition.x, _groundY, newPosition.z);
				// ^^ Move towards the player's last position ^^

					if(Vector3.Distance(agent.transform.position, _dashTarget) < _distanceThreshold + 0.6){ // Distance check for stopping + 0.6, due to the size of the player model
						EndAction(true);
					}

					_timer += Time.deltaTime;
					if (_timer >= _maxTimer){
					EndAction(true);
			}
		}

			

		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
			fireEyes.value.SetActive(false);
			normalEyes.value.SetActive(true);

		}

	}
}