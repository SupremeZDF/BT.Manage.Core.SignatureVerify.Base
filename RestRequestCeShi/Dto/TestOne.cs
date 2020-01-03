using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BT.Manage.Core.SigRestRequest.Base;
using BT.Manage.Frame.Base;

namespace RestRequestCeShi
{
    public class TestOne
    {
        public static void PostRequest()
        {
            SignatureRestRquest<T_User> signatureRestRquest = new SignatureRestRquest<T_User>();

            Model model = new Model()
            {
                ID = 1,
                Name = "打他",
                Psswd = "wewd",
                Sex = "男",
                grade = new List<TwoGrades>()
                {
                    new TwoGrades()
                    {
                       ID=1,
                       Nmae="男"
                    }
                }, 
            };

            var Grade = new List<TwoGrades>()
                {
                    new TwoGrades()
                    {
                       ID=1,
                       Nmae="男"
                    }
                };

            Dictionary<string, object> pairs = new Dictionary<string, object>();
            pairs.Add("ID", "1");
            pairs.Add("Name", "打他");
            pairs.Add("Psswd", "wewd");
            pairs.Add("Sex", "男");
            pairs.Add("grade", Newtonsoft.Json.JsonConvert.SerializeObject(Grade));

            //var i = signatureRestRquest.asyncRestRequestGet("yuyu123yu123q32yu", "http://localhost:49330/api/Login/TwoGetCeShi", null,pairs);
            //var j = signatureRestRquest.RestRequestPost<Model>("yuyu123yu123q32yu", Newtonsoft.Json.JsonConvert.SerializeObject(model), "http://localhost:49330/api/Login/OnePostCeShi");

            //var iResult = i.Result;
            //var jResult = j.Result;
        }
    }
}
