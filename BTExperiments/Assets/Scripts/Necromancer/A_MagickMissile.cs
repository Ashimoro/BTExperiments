using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class A_MagickMissile : ActionTask<Transform> {

		public BBParameter<GameObject> missilePrefab;
		public BBParameter<float> spawnDelay;

		private float _timer;

		
		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {

			_timer = 0f;

			


		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
			_timer += Time.deltaTime;
			Vector3 spawnPosition = agent.transform.position + Vector3.forward;

			if (_timer >= spawnDelay.value) {
				GameObject missileBolt = UnityEngine.Object.Instantiate(missilePrefab.value, spawnPosition, Quaternion.identity);
				EndAction(true);
			}

		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

	}
}