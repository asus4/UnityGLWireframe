using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// GL drawer camera.
/// </summary>
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Wireframe/GL Drawer Camera")]
public class GLDrawerCamera : MonoBehaviour {
	
	[SerializeField]
	Material mat;
	
	List<GLDrawerBase> drawers;
	
	[SerializeField]
	int antiAliasing = 2;
	
	Camera _camera;
	static GLDrawerCamera _mainGLCamera;
	
	void Awake () {
		_mainGLCamera = this;
		drawers = new List<GLDrawerBase>();
	}
	
	void Start ()
	{
		QualitySettings.antiAliasing = antiAliasing;
		
		if(mat == null) {
			mat = new Material(Shader.Find("Wireframe/ColoredBlendedLine"));
		}
		
		// cache
		_camera = this.camera;
	}
	
	void OnPreRender ()
	{
		GL.wireframe = true;
	}

	void OnPostRender ()
	{
		GL.wireframe = false;
		
		GL.PushMatrix ();
		mat.SetPass (0);
		
		Matrix4x4 mtx = _camera.cameraToWorldMatrix;
		foreach (var drawer in drawers) {
			drawer.Draw (mtx);
		}
		
		GL.PopMatrix ();
	}
	
	public void AddDrawer(GLDrawerBase drawer)
	{
		drawers.Add(drawer);
	}
	
	public void RemoveDrawer(GLDrawerBase drawer)
	{
		drawers.Remove(drawer);
	}
	
	public static GLDrawerCamera mainGLCamera {
		get {
			if(_mainGLCamera == null) {
				_mainGLCamera = GLDrawerCamera.CreateAtCamera(Camera.main);
			}
			return _mainGLCamera;
		}
	}
	
	public static GLDrawerCamera CreateAtCamera(Camera cam) {
		return cam.gameObject.AddComponent<GLDrawerCamera>();
	}
}