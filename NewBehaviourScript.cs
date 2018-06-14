using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;

public class NewBehaviourScript : MonoBehaviour
{


    string clientID="Esp32";
    string brokerIpAddress = "192.168.0.108";
    public string msg;
    public float speed = 10.0F;
    private static int buttonState = 0;
    void Awake()
    {
        //connect to the broker

        MqttClient client = new MqttClient(brokerIpAddress);
        client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
        client.Connect(clientID);
        client.Subscribe(new string[] { "button" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
    }
    static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        string msg = Encoding.UTF8.GetString(e.Message);
        //Debug.Log(msg);
        buttonState = State(msg);
    }
    static int State(string msg)
    {
        if (msg == "0")
        {
            return 0;
        }
        else if (msg == "1")
        {
            return 1;
        }
        else
        {
            return 888;
        }

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Awake();
        Debug.Log(buttonState);
        if (buttonState == 1)
        {
            transform.Rotate(new Vector3(0, 0, 0));
        }
        else if (buttonState == 0)
        {
            transform.Rotate(new Vector3(speed, 0, 0));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, speed));
        }

    }
}
