using UnityEngine;
using System.Collections;

public class Sample : MonoBehaviour {
	
	[SerializeField]
	GLGraph graphX;
	
	[SerializeField]
	GLGraph graphY;
	
	[SerializeField]
	Transform rotateObj;
	
	float lastValue;	
	
	void Update () {
		// rescale mouseposition
		Vector3 v = Input.mousePosition;
		v -= new Vector3(Screen.width/2, Screen.height/2, 0);
		v.Scale(new Vector3(1.0f/Screen.width, 1.0f/Screen.width,0));
		
		// graph update
		graphX.AddValue(v.x);
		graphY.AddValue(v.y);
		
		// rotate
		Vector3 rot = new Vector3(v.y, v.x, 0);
		rotateObj.Rotate(rot);
		
		// ringbuffer
		lastValue = v.x;
	}
	
	void OnGUI() {
		GUILayout.Label("Current : " + lastValue);
		GUILayout.Label("Calibrated : " + GetCalibrated(lastValue));
	}
	
	float GetCalibrated(float value) {
		float min = float.MaxValue;
		float max = float.MinValue;
		
		var buffer = graphX.Buffer;
		
		foreach(float f in buffer) {
			if(f < min) {
				min = f;
			}
			if(max < f) {
				max = f;
			}
		}
		return Mathf.InverseLerp(min, max, value);
	}
	
}
