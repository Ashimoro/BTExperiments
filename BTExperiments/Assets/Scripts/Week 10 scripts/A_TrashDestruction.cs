using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class A_TrashDestruction : ActionTask<NavMeshAgent> {


		public BBParameter<GameObject> closestTrash;

		private float _stopDistance = 1.5f;
		protected override void OnExecute() {

			if (closestTrash.value != null){
			agent.SetDestination(closestTrash.value.transform.position); //moming to the closest trash
			}

		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
			float distance = Vector3.Distance(agent.transform.position, closestTrash.value.transform.position); //finding a distance to the object
			if (distance <= _stopDistance){
				
				UnityEngine.Object.Destroy(closestTrash.value); //when agent is close enough - destroying the trash
				EndAction(true);
			}



		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}