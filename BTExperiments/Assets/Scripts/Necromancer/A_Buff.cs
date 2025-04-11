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

		protected override void OnExecute() {

			_timer = 0f;

			effectSelf.SetActive(true); // Activating glowing effect

			
				
		}
        protected override void OnUpdate()
        {
            
			_timer += Time.deltaTime;

			if(_timer >= castTime){
				EndAction(true);
			}

        }

		// everything is supposed to be in "on stop", because I want to make a cast time first. This is possible to do everything inside of the timer as well, but I didn't like how it looks
		protected override void OnStop() { 
			float bestDistance = Mathf.Infinity; // Selecting maximum possible distance for it to be rewriten when the script will scan first enemy
			GameObject buffTarget = null;

			GameObject[] skeletonScanner = GameObject.FindGameObjectsWithTag("Skeleton"); // finding all skeletons on the field
			GameObject[] knightScanner = GameObject.FindGameObjectsWithTag("Knight"); // findinh a knight on the field
			enemiesList.value.Clear();

			// adding both of them to the one list
			foreach(GameObject obj in skeletonScanner) {
				enemiesList.value.Add(obj);
			}

			foreach(GameObject obj in knightScanner) {
				enemiesList.value.Add(obj);
			}


			// finding who is the closest to the necromancer
			foreach (GameObject enemy in enemiesList.value) {
				float currentDistance = Vector3.Distance(agent.position, enemy.transform.position);

				if (currentDistance < bestDistance) {
					bestDistance = currentDistance;
					buffTarget = enemy;
				}
			}

				target.value = buffTarget;

				// depending on who is closer, giving different speed boost and making a telegraphy over their head to show who is buffed
				if (target.value.CompareTag("Knight")){
					NavMeshAgent enemyNM = target.value.GetComponent<NavMeshAgent>();
					enemyNM.speed = 8f;
					
					Vector3 offset = new Vector3(0,2,0);
					ParticleSystem newEffect = GameObject.Instantiate(effectTarget, target.value.transform.position + offset,Quaternion.identity); // creating particle system on the field
					newEffect.transform.SetParent(target.value.transform); // making a knight or skeleton a new parent for particle system to follow them
					
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