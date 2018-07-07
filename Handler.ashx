<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Xml;
using Newtonsoft.Json;

public class Handler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(context.Server.MapPath("~/trees/XMLFile.xml"));


        string gameCode = context.Request["gameCode"];

        XmlNode gameNode = myDoc.SelectSingleNode("//game[@code='" + gameCode + "']");

        if (gameNode != null && gameNode.Attributes["published"].Value == "true")
        {
            string jsonText = JsonConvert.SerializeXmlNode(gameNode);

            context.Response.Write(jsonText);
        }
        else if(gameNode != null && gameNode.Attributes["published"].Value == "false"){
            context.Response.Write("קיים משחק אך אינו מפורסם");
        }
        else
        {
            context.Response.Write("לא נמצא משחק");
        }
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}
















