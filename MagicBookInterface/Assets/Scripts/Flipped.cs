using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipped : StateMachineBehaviour {

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.SetBool("Flipped", true);
	}
}
