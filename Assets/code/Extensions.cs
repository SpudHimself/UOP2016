﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class Extensions
{
	public static void SetTagRecursively( this GameObject gameObject, string tag )
	{
		for ( int i = 0; i < gameObject.transform.childCount; i++ )
		{
			gameObject.transform.GetChild( i ).gameObject.SetTagRecursively( tag );
		}

		gameObject.tag = tag;
	}
}