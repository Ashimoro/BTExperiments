using System.Collections.Generic;
using System.Threading;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class A_SkeletonSpawn : ActionTask {

		// Skeleton creation
		public BBParameter<GameObject> skeletonPrefab;
		public BBParameter<List<GameObject>> skeletonList;

		// Field size
		private Vector3 _spawnAreaMin = new Vector3(-10, 0, -10);
        private Vector3 _spawnAreaMax = new Vector3(10, 0, 10);

		// Timer
		public float spawnInteval = 0.5f;
		private float _timer;

		// Effect
		public BBParameter<GameObject> particlesController;
		protected override void OnExecute() {
			_timer = 0f;

			skeletonList.value.Clear(); // clearing list everytime, to remove dead skeletons

			particlesController.value.SetActive(true);
		}

		protected override void OnUpdate() {
			
			_timer += Time.deltaTime;

			if (skeletonList.value.Count >= 3){
				EndAction(true);
			}

			if (_timer >= spawnInteval) {
				float x = Random.Range(_spawnAreaMin.x, _spawnAreaMax.x); // every skeleton must spawn from new random position on the screen, so i'm recalculating it every time
				float z = Random.Range(_spawnAreaMin.z, _spawnAreaMax.z);

				Vector3 spawnPoint = new Vector3(x, 1, z);

				GameObject skeleton = UnityEngine.Object.Instantiate(skeletonPrefab.value, spawnPoint, Quaternion.identity);
				skeletonList.value.Add(skeleton); // adding them to the list in case that something will break, and this script will play one more time.

				_timer = 0f;

				if(skeletonList.value.Count >= 3){
					EndAction(true);

				}

			}


		}

		protected override void OnStop() {
			particlesController.value.SetActive(false);
			
		}
	}
}