using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class A_MagickMissile : ActionTask<Transform> {

		public BBParameter<GameObject> missilePrefab;
		public GameObject effect;


		public BBParameter<float> spawnDelay;
		private float _timer;


		protected override void OnExecute() {

			_timer = 0f;
			effect.SetActive(true);
			


		}

		protected override void OnUpdate() {
			
			_timer += Time.deltaTime; 
			Vector3 spawnPosition = agent.transform.position + agent.forward; // giving a spawn point in front of the necromancer

			if (_timer >= spawnDelay.value) {
				GameObject missileBolt = UnityEngine.Object.Instantiate(missilePrefab.value, spawnPosition, Quaternion.identity); // creating missile
				EndAction(true);
			}

		}

		protected override void OnStop() {
			effect.SetActive(false);
		}

	}
}