using System.Collections;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


public class test_thread : MonoBehaviour
{
    // Start is called before the first frame update
    UdpClient client;
    void Start()
    {
        client = new UdpClient(5500);
        StartCoroutine(LoopFunction(0.001f));
    }




private IEnumerator LoopFunction(float waitTime)
{
    while (true)
    {
  
        yield return new WaitForSeconds(waitTime);
        //Second Log show passed waitTime (waitTime is float type value ) 
        UDPTest();
    


    }
}

private void UDPTest()
    {

        try
        {
            client.Connect("127.0.0.1", 5200);

            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 5200);
byte[] receiveBytes = client.Receive(ref remoteEndPoint);
            string receivedString = Encoding.ASCII.GetString(receiveBytes);
print("Message received from the server \n " + receivedString);


            byte[] sendBytes = Encoding.ASCII.GetBytes(receivedString);
            client.Send(sendBytes, sendBytes.Length);

        }
        catch(Exception e)
        {
            print("Exception thrown " + e.Message);
        }
    }



}



