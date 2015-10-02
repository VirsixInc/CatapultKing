using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Transform)), CanEditMultipleObjects]
public class HealthbarSceneView : Editor {

	UnityEditor.SceneView sceneView;
	Healthbar hb;
	//Transform player;
	Event cur;

	void Awake (){
		hb = FindObjectOfType<Healthbar>();
		//player = GameObject.FindGameObjectWithTag("Player").transform;
		if (hb == null)
			Debug.Log("Healthbar not in scene!");
	}

	public void OnSceneGUI() {

		sceneView = UnityEditor.SceneView.lastActiveSceneView;

		cur = Event.current;

		switch (cur.keyCode) {
		case KeyCode.H:
			if (Selection.activeGameObject != hb.gameObject)
				Selection.activeObject = hb.gameObject;
			//sceneView.LookAtDirect(player.position, player.rotation);
			break;
		case KeyCode.KeypadMinus:
			SceneView.RepaintAll();
			sceneView.size *= 1.1f;
			break;
		}
		 

	}
}
