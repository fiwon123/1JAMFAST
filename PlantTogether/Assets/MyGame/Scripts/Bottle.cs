﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bottle : InteractObject
{

	public Image fill;

	int maxWaters = 3;
	int waters = 0;

	PhotonView photonView;

	private void Start() {
		fill.fillAmount = waters / maxWaters;
		photonView = GetComponent<PhotonView>();
	}


	public override void DoAction(Player player) {
		throw new System.NotImplementedException();
	}

	public int GetWaters() {
		return waters;
	}

	public void SpendWater() {
		photonView.RPC("SpendWaterNetwork", RpcTarget.All);
	}

	[PunRPC]
	private void SpendWaterNetwork() {
		if (waters > 0) {
			waters--;
			fill.fillAmount = (float)waters / maxWaters;
			Debug.Log("Diminuiu para " + waters);
		}
	}

	public void Fill() {
		if (waters < maxWaters) {
			FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Encher Balde");
			waters++;
			fill.fillAmount = (float)waters / maxWaters;
			Debug.Log("Aumentou para " + waters);
		}
	}
}
