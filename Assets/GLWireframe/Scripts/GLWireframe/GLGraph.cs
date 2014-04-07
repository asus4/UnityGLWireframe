using UnityEngine;
using System.Collections.Generic;

public class GLGraph : GLDrawerBase {
	
	[SerializeField]
	int count;
	
	[SerializeField]
	Vector2 scale;
	
	[SerializeField]
	Color color;
	
	GLWireframe.RingBuffer<float> buffer;
	
	void Start()
	{
		buffer = new GLWireframe.RingBuffer<float>(count);
	}
	
	protected override void OnDraw ()
	{
		GL.Begin (GL.LINES);
		GL.Color (color);
		
		int i=0;
		Vector3 last = Vector3.zero;
		foreach(float f in buffer) {
			if(i == 0) {
				last = new Vector3(scale.x * (i-count/2) , f * scale.y, 0);
				GL.Vertex(last);
			}
			else {
				GL.Vertex(last);
				last = new Vector3(scale.x * (i-count/2) , f * scale.y, 0);
			}
			GL.Vertex(last);
			i++;
		}
		if(i != 0) {
			GL.Vertex(last);
		}
		GL.End ();
	}
	
	public void AddValue(float value) {
		buffer.Push(value);
	}
	
	public GLWireframe.RingBuffer<float> Buffer {
		get {
			return buffer;
		}
	}
}
