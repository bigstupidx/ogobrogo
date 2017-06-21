using UnityEngine;
using System.Collections;

public class RigidBodyToggler : MonoBehaviour 
{
	public int IgnoreLayer = 8;
	public int PlayerLayer = 9;

	public bool PowerUp
	{
		set
		{
			setPowerUp(value);
		}
	}

	private void setPowerUp(bool isPowerUp)
	{
		if(isPowerUp)
		{
			setLayer(PlayerLayer);
		}
		else
		{
			setLayer(IgnoreLayer);
		}
	}

	private void setLayer(int layer) {
		gameObject.layer = layer;

		foreach (Transform child in transform) {
			child.gameObject.layer = layer;
		}
	}
}
