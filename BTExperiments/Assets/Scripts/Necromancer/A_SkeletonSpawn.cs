using System.Collections.Generic;
using System.Threading;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class A_SkeletonSpawn : ActionTask {

		public BBParameter<GameObject> skeletonPrefab;
		public BBParameter<GameObject> particlesController;
		public BBParameter<List<GameObject>> skeletonList;

		public float spawnInteval = 0.5f;

		private Vector3 _spawnAreaMin = new Vector3(-10, 0, -10);
        private Vector3 _spawnAreaMax = new Vector3(10, 0, 10);

		private float _timer;

		protected override void OnExecute() {
			_timer = 0f;

			skeletonList.value.Clear();

			particlesController.value.SetActive(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
			_timer += Time.deltaTime;

			if (skeletonList.value.Count >= 3){
				EndAction(true);
			}

			if (_timer >= spawnInteval) {
				float x = Random.Range(_spawnAreaMin.x, _spawnAreaMax.x);
				float z = Random.Range(_spawnAreaMin.z, _spawnAreaMax.z);

				Vector3 spawnPoint = new Vector3(x, 1, z);

				GameObject skeleton = UnityEngine.Object.Instantiate(skeletonPrefab.value, spawnPoint, Quaternion.identity);
				skeletonList.value.Add(skeleton);

				_timer = 0f;

				if(skeletonList.value.Count >= 3){
					EndAction(true);

				}

			}


		}

		//Called when the task is disabled.
		protected override void OnStop() {
			particlesController.value.SetActive(false);
			
		}
	}
}