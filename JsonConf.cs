using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class JsonConf2
{
    public string model;
    public JsonConf1[] messages;
}

[System.Serializable]
public class JsonConf1
{

    public string role = "user";
    public string content;
}

[System.Serializable]
public class JsonConf3
{
    public string id;
    public string _object;
    public string created;
    public JsonConf4[] choices;
    public string usage;

}
[System.Serializable]
public class JsonConf4
{
    public string index;
    public JsonConf1 message;
    public string finish_reason;

}