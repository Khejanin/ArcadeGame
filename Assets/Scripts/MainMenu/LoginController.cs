using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;
using WebSocketSharp;
using Object = UnityEngine.Object;

namespace MainMenu
{
    public class LoginController : MonoBehaviour
    {
        private WebSocket _ws;
        private List<QRRenderer> _qrCodes;
        private string _token = "c9b92054-8943-4b58-af81-29491df689f3";


        // Start is called before the first frame update
        void Start()
        {
            this._qrCodes = Object.FindObjectsOfType<QRRenderer>().ToList();

            this._ws = new WebSocket("wss://node.xanlosh.us");

            this._ws.OnOpen += (sender, e) => { Debug.Log("Connected Successfully!"); };

            this._ws.OnError += (sender, e) => { Debug.Log(e.Message); };

            this._ws.OnMessage += OnMessage;

            this._ws.Connect();

            this._ws.Send("{\"login\" : \"" + _token + " \"}");
        }

        private void OnMessage(object sender, MessageEventArgs m)
        {
            Debug.Log(m.Data);
            JObject json;
            try
            {
                json = JObject.Parse(m.Data);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return;
            }

            Debug.Log(json["type"]);
            if (json["type"].ToString() == "login")
            {
                QRRenderer qrRenderer = this._qrCodes.Find(e => e.CompareToken(json["data"]["token"].ToString()));
                if (qrRenderer != null)
                {
                    qrRenderer.SetWelcomeText(json["data"]["user"].ToString());
                }
            }
        }
    }
}