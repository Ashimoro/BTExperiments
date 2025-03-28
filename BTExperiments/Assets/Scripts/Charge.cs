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

		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			
			fireEyes.value.SetActive(true);

			Vector3 rotationDirection = playerLocation.value.transform.position - agent.transform.position;
			rotationDirection.y = 0;
			agent.transform.rotation = Quaternion.LookRotation(rotationDirection);

			EndAction(true);

		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
			


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