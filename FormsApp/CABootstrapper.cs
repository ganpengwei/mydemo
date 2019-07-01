using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using System;
using System.Text;

namespace FormsApp
{
    public class CABootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            pipelines.BeforeRequest += (x) =>
            {
                return null;
            };

            pipelines.AfterRequest += (x) =>
            {
                if (x.Request.Url.Path.ToLower() == "/api" || x.Request.Url.Path.ToLower() == "/doc")
                    x.Response.WithContentType("application/pdf");
                bool isView = false; //判断返回数据是否为 view text/html 否则  转换 application/json
                if (x.Response.Contents.Target.ToString() == "Nancy.Responses.MaterialisingResponse")
                {
                    var r = (Nancy.Responses.MaterialisingResponse)x.Response.Contents.Target;
                    if (r.ContentType == "text/html")
                    {
                        isView = true;
                    }
                }
                if (!isView)
                    x.Response.WithContentType("application/json;charset=UTF-8");
                x.Response.WithHeader("Access-Control-Allow-Origin", "*");
                x.Response.WithHeader("Access-Control-Allow-Methods", "GET");
            };

            pipelines.OnError += Error;

            base.RequestStartup(container, pipelines, context);
        }

        private dynamic Error(NancyContext ctx, Exception ex)
        {
            var exMsg = new
            {
                ResCode = "-2",
                ResMsgs = ex.Message + (ex.InnerException == null ? "" : ex.InnerException.Message)
            };

            return new Response()
            {
                StatusCode = HttpStatusCode.OK,
                ContentType = "application/json;charset=UTF-8",
                Contents = (s) =>
                {
                    var exMsgEncode = Encoding.UTF8.GetBytes(exMsg.ResMsgs);
                    s.Write(exMsgEncode, 0, exMsgEncode.Length);
                }
            };
        }
    }
}
