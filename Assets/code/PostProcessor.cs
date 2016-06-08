using UnityEngine;
using System.Collections;

public class PostProcessor : MonoBehaviour {
	private Material mMat;

	private float mGreyPhase;

	void Start() {
		mMat = new Material( Shader.Find( "Custom/PostProcess" ) );
	}
	
	void OnRenderImage( RenderTexture source, RenderTexture destination ) {
		mMat.SetFloat( "_FadePhase", mGreyPhase );

		Graphics.Blit( source, destination, mMat );
	}
}