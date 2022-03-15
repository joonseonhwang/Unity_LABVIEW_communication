using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;
//test github
public class UDPConnector : MonoBehaviour
{
    UdpClient ReceivePort, SendPort;
    IPEndPoint remoteEndPoint;

    public class MyUDPEvent : UnityEvent<string> {}
    public MyUDPEvent OnReceiveMessage = new MyUDPEvent ();

    void Start() {

        OnReceiveMessage.AddListener (receiveMsg);
    }

    void Init() {
        ReceivePort = new UdpClient (5500);
        ReceivePort.BeginReceive (OnReceive, null);
        Send ();
    }


    public void Send (string msg = "receive from unity") {
        SendPort = new UdpClient ();
        string remoteIP = IF_SentIP.text;
        int remotePort = Int32.Parse(IF_SendPort.text);
        remoteEndPoint = new IPEndPoint (IPAddress.Parse (remoteIP), remotePort);

        byte[] data = System.Text.Encoding.UTF8.GetString (msg);
        SendPort.Send (data, data.Length, remoteEndPoint);

    }

    void OnReceive (IAsyncResult ar){
        try{
            IPEndPoint ipEndPoint = null;
            byte[] data = ReceivePort.EndReceive (ar, ref ipEndPoint);
            message = System.Text.Encoding.UTF8.GetString (data);
        } catch (SocketException e) { }

        ReceivePort.BeginReceive (OnReceive, null);
    }

    void receiveMsg (string msg){
        Txt_Message.text = Txt_Message.text + msg + "\n";
    }

    void Update() {

        OnReceiveMessage.Invoke (message);
    }

    public void ShutDown () {
        OnReceiveMessage.RemoveAllListeners();
        if (ReceivePort !=null)
            ReceivePort.Close ();
        if (SendPort != null)
            SendPort.Close ();
        ReceivePort = null;
        SendPort = null;
    }

    }
