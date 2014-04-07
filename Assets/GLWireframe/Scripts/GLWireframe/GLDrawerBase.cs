using UnityEngine;

[AddComponentMenu("")] // hide in component
public abstract class GLDrawerBase : MonoBehaviour {
	
	[SerializeField]
	GLDrawerCamera drawerCamera;
	
	protected virtual void OnEnable() {
		if(drawerCamera == null) {
			drawerCamera = GLDrawerCamera.mainGLCamera;
		}
		drawerCamera.AddDrawer(this);
	}
	
	protected virtual void OnDisable() {
		drawerCamera.RemoveDrawer(this);
	}
	
	public void Draw (Matrix4x4 camMatrix)
	{
		if (!this.enabled) {
			return;
		}
		
		GL.PushMatrix ();
		GL.MultMatrix (camMatrix);
		GL.MultMatrix (transform.localToWorldMatrix);
		OnDraw ();
		GL.PopMatrix ();
	}
	
	/// <summary>
	/// override this method to Draw GL
	/// </summary>
	protected abstract void OnDraw ();
}
