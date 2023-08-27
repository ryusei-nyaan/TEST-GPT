using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChatGPTAPI : MonoBehaviour
{
    public Text Test_Resp;
    private string Req;
    private InputField Input_Field;
    private Json_Conf2 Json_;
    private string apiEndpoint = "https://api.openai.com/v1/chat/completions";
    private string apiKey = "";

    void Start()
    {
        Input_Field = GameObject.Find("InputField (Legacy)").GetComponent<InputField>();
        string InpSt = Resources.Load<TextAsset>("GPT").ToString();
        Json_ = Json_Utility.FromJson_<Json_Conf2>(InpSt);

    }

    public void OnClick()
    {
        Req = Input_Field.text;
        StartCoroutine(GetResponseFromChatGPTAPI(Req));

    }



    // ChatGPT APIにテキストを送信してレスポンスを取得する
    public IEnumerator GetResponseFromChatGPTAPI(string text)
    {
        // ヘッダーを設定
        var headers = new Dictionary<string, string>()
        {
            {"Content-Type", "application/Json_"},
            {"Authorization", "Bearer " + apiKey},
        };


        Json_.messages[0].content = text;
        string Json_Data = Json_Utility.ToJson_(Json_);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(Json_Data);
        
        
        // リクエストを送信
        UnityWebRequest Request = new UnityWebRequest(apiEndpoint, "POST");
        Request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        Request.downloadHandler = new DownloadHandlerBuffer();
        foreach (KeyValuePair<string, string> header in headers)
        {
            Request.SetRequestHeader(header.Key, header.Value);
        }
        

        yield return Request.SendWebRequest();
        
        string responseText = Request.downloadHandler.text;
        responseText.Replace("object", "_object");
        Json_Conf3 responseText2 = Json_Utility.FromJson_<Json_Conf3>(responseText);
        Test_Resp.text = "";
        
        
        for (int i =0;i < responseText2.choices[0].message.content.Length;i++)
        {
            Test_Resp.text += responseText2.choices[0].message.content[i];
            for (var j = 0; j <90; j++)
            {
                yield return null;
            }
            
        }
        //Test_Resp.text = responseText2.choices[0].message.content;
        
        Input_Field.text = "";
    }
}
