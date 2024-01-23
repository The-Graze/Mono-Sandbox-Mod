using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


class NetMan : MonoBehaviourPunCallbacks
{

    void Start()
    {
        Destroy(transform.GetChild(0));
        foreach (Component c in transform.GetComponents(typeof(Component)))
        {
            if (!photonView.ObservedComponents.Contains(c))
            {
                _cAdd(c);
            }
        }
        Destroy(transform.GetChild(0));
    }
    void _cAdd(Component c)
    {
        photonView.ObservedComponents.Add(c);
    }
}