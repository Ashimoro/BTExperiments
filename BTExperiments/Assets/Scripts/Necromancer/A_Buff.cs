using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class A_Buff : ActionTask<Transform>{

		
		public BBParameter<GameObject> target;
		public BBParameter<List<GameObject>> enemiesList;

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {

			float bestDistance = Mathf.Infinity;
			GameObject buffTarget = null;

			GameObject[] skeletonScanner = GameObject.FindGameObjectsWithTag("Skeleton");
			GameObject[] knightScanner = GameObject.FindGameObjectsWithTag("Knight");
			enemiesList.value.Clear();

			foreach(GameObject obj in skeletonScanner) {
				enemiesList.value.Add(obj);
			}

			foreach(GameObject obj in knightScanner) {
				enemiesList.value.Add(obj);
			}



			foreach (GameObject enemy in enemiesList.value) {
				float currentDistance = Vector3.Distance(agent.position, enemy.transform.position);

				if (currentDistance < bestDistance) {
					bestDistance = currentDistance;
					buffTarget = enemy;
				}
			}

				target.value = buffTarget;

				if (target.value.CompareTag("Knight")){
					NavMeshAgent enemyNM = target.value.GetComponent<NavMeshAgent>();
					enemyNM.speed = 8f;
					
				}

				if (target.value.CompareTag("Skeleton")){
					NavMeshAgent enemyNM = target.value.GetComponent<NavMeshAgent>();
					enemyNM.speed = 10f;

				}
			
			EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}