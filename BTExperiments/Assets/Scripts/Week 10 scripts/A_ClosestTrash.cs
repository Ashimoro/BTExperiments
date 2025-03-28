using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class A_ClosestTrash : ActionTask<Transform> {

		public BBParameter<List<GameObject>> trashList;
		public BBParameter<GameObject> closestTrash;

		


		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			
		float bestDistance = Mathf.Infinity;
		GameObject bestTrash = null;

			foreach(GameObject trash in trashList.value){ //checking every trash in array
			float currentDistance = Vector3.Distance(agent.position, trash.transform.position); //calculating distance from agent to trash

			if (currentDistance < bestDistance) { // updating bestDistance and bestTrash, if object in a list closer to the player
				bestDistance = currentDistance;
				bestTrash = trash;
			}
			}
			

			closestTrash.value = bestTrash; //sending new trash info to BB
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