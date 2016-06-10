using UnityEngine;
using System.Collections;

public class PostProcessor : MonoBehaviour {
	private Material mMat;

	private float mGreyPhase = 0f;

	void Awake()
	{
		mMat = new Material( Shader.Find( "Custom/PostProcess" ) );
	}

	public float GetGreyScale()
	{
		return mGreyPhase;
	}

	public void SetGreyScale( float greyScale )
	{
		mGreyPhase = greyScale;
	}

	void OnRenderImage( RenderTexture source, RenderTexture destination ) {
		mMat.SetFloat( "_FadePhase", mGreyPhase );

		Graphics.Blit( source, destination, mMat );
	}
}