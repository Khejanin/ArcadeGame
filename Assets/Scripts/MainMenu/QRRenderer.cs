using System;
using TMPro;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

namespace MainMenu
{
    // ReSharper disable once InconsistentNaming
    public class QRRenderer : MonoBehaviour
    {
        public int qrPosX;
        public int qrPosY;
        public LoginController loginController;
        private Texture2D _qrCode;
        public TextMeshProUGUI welcomeText;
        private Rect _screenRect;
        private string _playerToken;

        public bool CompareToken(string token)
        {
            return token == _playerToken;
        }

        // Start is called before the first frame update
        private void Start()
        {
            _playerToken = Guid.NewGuid().ToString();
            string uri = "https://node.xanlosh.us/login?token=" + _playerToken;
            Debug.Log(uri);
            this._qrCode = GenerateQr(uri);
        }

        private static Color32[] Encode(string textForEncoding,
            int width, int height)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = height,
                    Width = width
                }
            };
            return writer.Write(textForEncoding);
        }

        private static Texture2D GenerateQr(string text)
        {
            var encoded = new Texture2D(256, 256);
            var color32 = Encode(text, encoded.width, encoded.height);
            encoded.SetPixels32(color32);
            encoded.Apply();
            return encoded;
        }

        private void OnGUI()
        {
            GUI.Button(new Rect(qrPosX, qrPosY, 256, 256), this._qrCode, GUIStyle.none);
        }

        public void SetWelcomeText(string playerName)
        {
            this.welcomeText.SetText("Welcome " + playerName);
        }
    }
}