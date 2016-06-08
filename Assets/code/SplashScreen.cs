using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
	private GUITexture mTexture;

	private float mAlpha = 0f;
	private bool mFadingIn = true;

	private Color color;

	void Start()
	{
		mTexture = GetComponent<GUITexture>();
	}

	void Update()
	{
		FadeInAndOut();
	}

	private void FadeInAndOut()
	{
		if ( mFadingIn )
		{
			UpdateAlpha( 0.02f );
		}

		if ( mTexture.color.a >= 1f )
		{
			mFadingIn = false;
			mAlpha = 1f;
			StartCoroutine( FadeOut() );
		}

		if ( Input.anyKeyDown )
		{
			Application.LoadLevel( "Main" );
		}
	}

	public void UpdateAlpha( float a )
	{
		mAlpha += a;
		mTexture.color = new Color( mTexture.color.r, mTexture.color.g, mTexture.color.b, mAlpha );
	}

	public IEnumerator FadeOut()
	{
		yield return new WaitForSeconds( 2 );

		if ( mAlpha >= 0f )
		{
			UpdateAlpha( -0.02f );

			if ( mAlpha <= 0 )
			{
				mAlpha = 0;
				Application.LoadLevel( "Main" );
			}
		}
	}
}