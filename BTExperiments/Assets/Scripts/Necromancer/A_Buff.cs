using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class A_Buff : ActionTask<Transform>{

		
		public BBParameter<GameObject> target;
		public BBParameter<List<GameObject>> enemiesList;
		public GameObject effectSelf;
		public ParticleSystem effectTarget;
		public float castTime = 1f;
		private float _timer;


		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {

			_timer = 0f;

			effectSelf.SetActive(true);

			
				
		}
        protected override void OnUpdate()
        {
            
			_timer += Time.deltaTime;

			if(_timer >= castTime){
				EndAction(true);
			}

        }
		protected override void OnStop() {
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
					
					Vector3 offset = new Vector3(0,2,0);
					ParticleSystem newEffect = GameObject.Instantiate(effectTarget, target.value.transform.position + offset,Quaternion.identity);
					newEffect.transform.SetParent(target.value.transform);
					
				}

				if (target.value.CompareTag("Skeleton")){
					NavMeshAgent enemyNM = target.value.GetComponent<NavMeshAgent>();
					enemyNM.speed = 10f;
					ParticleSystem newEffect = GameObject.Instantiate(effectTarget, target.value.transform.position,Quaternion.identity);
					newEffect.transform.SetParent(target.value.transform);

				}
			


			effectSelf.SetActive(false);


		}



	}
}