using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Unity.VisualScripting;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class C_TrashListChecker : ConditionTask {

		public BBParameter<List<GameObject>> trash;


		protected override string OnInit(){
			return null;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable() {
			
		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
			GameObject[] trashSearcher = GameObject.FindGameObjectsWithTag("Trash"); //finding all object with certain tag
			trash.value.Clear(); //clearing list to not overflow it (because this script looking for object every frame, this is required, to not have 100 coppies of the same object)

			foreach (GameObject obj in trashSearcher) {
				trash.value.Add(obj); //adding all object with the tag to the list
			}

			return trash.value.Count > 0;


		}
	}
}