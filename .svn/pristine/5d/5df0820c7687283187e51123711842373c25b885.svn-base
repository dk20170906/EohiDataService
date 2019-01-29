using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EohiDataCenter.Lib.NetWork
{
    public class OAuth
    {
        

        public static ClientClass.ApiResult InvokeApi(string url, string ProjectId, List<ClientClass.ParmField> aparmList)
        {
            aparmList.Add(new ClientClass.StringField("projectid", ProjectId));
            return InvokeApi(url, aparmList);
        }

        public static ClientClass.ApiResult InvokeApi(string url, List<ClientClass.ParmField> aparmList)
        {

            ClientClass.ApiResult result = new ClientClass.ApiResult();
            HttpClient client = new HttpClient();
            
            Task<HttpResponseMessage> resultTask;
            try
            {
                List<ClientClass.ParmField> curParmList = new List<ClientClass.ParmField>();
                if (aparmList != null)
                    curParmList = aparmList;
                curParmList.Add(new ClientClass.StringField("loginAccount", Config.UserToken.User_Account));
                curParmList.Add(new ClientClass.StringField("loginID", Config.UserToken.User_ID));
                curParmList.Add(new ClientClass.StringField("loginName", Config.UserToken.User_Name));
                curParmList.Add(new ClientClass.StringField("loginToken", Config.UserToken.User_Token));
                curParmList.Add(new ClientClass.StringField("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")));

                string boundary = "------------------------------NFERP------------------------------";
                byte[] bodybyte = getBody(curParmList, boundary);
                HttpContent content = new ByteArrayContent(bodybyte, 0, bodybyte.Length);
                content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("multipart/form-data;boundary=" + boundary);                
                resultTask = client.PostAsync(url, content);                
                resultTask.Wait();
                result.statusCode = (int)resultTask.Result.StatusCode;
                result.result = result.statusCode == 200;
                if (resultTask.Result.Content.Headers.ContentType.MediaType.ToLower() == "application/json" || resultTask.Result.Content.Headers.ContentType.MediaType.ToLower() == "text/html")
                {
                    Task<string> strTask = resultTask.Result.Content.ReadAsStringAsync();
                    strTask.Wait();
                    result.responseContent = strTask.Result;
                    result.total = 0;
                    if (result.result)
                    {
                        result.responseObject = JObject.Parse(result.responseContent);
                        if (result.responseObject["code"].Value<int>() != 1)
                        {
                            result.code = result.responseObject["code"].Value<int>();
                            result.result = false;
                            result.msg = result.responseObject["msg"].Value<string>();
                        }
                        else
                        {
                            result.code = result.responseObject["code"].Value<int>();
                            result.msg = result.responseObject["msg"].Value<string>();
                            if (result.responseObject["total"] != null)
                            {
                                result.data = result.responseObject["data"];
                                result.total = result.responseObject["total"].Value<long>();
                            }
                        }
                    }
                    else
                    {
                        if (result.statusCode == 200)
                        {
                            result.msg = result.responseContent;
                        }
                        else
                        {
                            result.msg = "网络请求错误：" + resultTask.Result.ReasonPhrase;
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                result.result = false;
                result.statusCode = -1;
                result.msg = ex.Message;
                result.responseContent = ex.Message;
                return result;
            }
            finally
            {
                client.Dispose();
            }
        }

        private static byte[] getBody(List<ClientClass.ParmField> aparmList, string boundary)
        {
            List<byte> bodylist = new List<byte>();
            UTF8Encoding encoding = new UTF8Encoding();
            string crlf = "\r\n";
            string charset = "utf-8";
            byte[] temp;
            foreach (ClientClass.ParmField pf in aparmList)
            {
                string body = "";
                if (pf.GetType() == typeof(ClientClass.StringField))
                {
                    body += "--" + boundary;
                    body += crlf;
                    body += "Content-Disposition:form-data;name=\"" + pf.Key + "\"";
                    body += crlf;
                    body += "Content-Type:text/plain;charset=" + charset;
                    body += crlf + crlf;
                    body += pf.Value;
                    body += crlf;
                    temp = encoding.GetBytes(body);
                    foreach (byte _b in temp)
                    {
                        bodylist.Add(_b);
                    }
                }
                else if (pf.GetType() == typeof(ClientClass.FileField))
                {
                    body += "--" + boundary;
                    body += crlf;
                    body += "Content-Disposition:form-data;name=\"" + pf.Key + "\";filename=\"" + pf.Value + "\"";
                    body += crlf;
                    body += "Content-Type:application/octet-stream";
                    body += crlf;
                    body += "Content-Transfer-Encoding:binary";
                    body += crlf + crlf;
                    temp = encoding.GetBytes(body);
                    foreach (byte _b in temp)
                    {
                        bodylist.Add(_b);
                    }
                    FileStream fileStream = new FileStream(pf.Value, FileMode.Open, FileAccess.Read);
                    byte[] bytefile = new byte[fileStream.Length];
                    fileStream.Position = 0;
                    fileStream.Read(bytefile, 0, Convert.ToInt32(fileStream.Length));
                    foreach (byte _b in bytefile)
                    {
                        bodylist.Add(_b);
                    }
                    temp = encoding.GetBytes(crlf);
                    foreach (byte _b in temp)
                    {
                        bodylist.Add(_b);
                    }
                }
            }
            temp = encoding.GetBytes("--" + boundary + "--" + crlf);
            foreach (byte _b in temp)
            {
                bodylist.Add(_b);
            }
            return bodylist.ToArray();
        }
    }
}
