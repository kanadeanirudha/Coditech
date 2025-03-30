using Coditech.Common.API.Model;
using Coditech.Common.Logger;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text;


namespace Coditech.Hangfire
{
    public class CoditechDeleteLogMessageHelper : BaseScheduler, ISchedulerProviders
    {
        protected readonly ICoditechLogging coditechLogging;
        private readonly IServiceProvider _serviceProvider;
        public CoditechDeleteLogMessageHelper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            coditechLogging = _serviceProvider.GetService<ICoditechLogging>();
        }
        private string TokenValue = string.Empty;
        public void InvokeMethod(TaskSchedulerModel model)
        {
            if (!string.IsNullOrEmpty(model?.ExeParameters))
            {
                string[] args = model.ExeParameters.Split(',');
                try
                {
                    // Request timeout is contained in args[`   ].
                    if (args.Length > 3 && !string.IsNullOrEmpty(args[2]))
                        base.RequestTimeout = int.Parse(args[2]);
                }
                catch (Exception ex)
                {
                    coditechLogging.LogMessage(ex.Message, LoggingComponent, TraceLevel.Warning);
                }

                CallAPI(args);
            }
        }

        private void CallAPI(string[] args)
        {
            string data = JsonConvert.SerializeObject(new ParameterModel());
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            string jsonString = string.Empty;
            string message = string.Empty;
            string requestPath = $"{args[0]}/LogMessage/DeleteLogMessage";
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestPath);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Timeout = base.RequestTimeout;
                request.ContentLength = dataBytes.Length;
                request.Headers.Add($"{AuthorizationHeader}: {"Basic " + args[1]}");
                request.Headers.Add($"{UserHeader}: {args[2]}");
                using (var reqStream = request.GetRequestStream())
                {
                    reqStream.Write(dataBytes, 0, dataBytes.Length);
                }
                using (HttpWebResponse responce = (HttpWebResponse)request.GetResponse())
                {
                    Stream datastream = responce.GetResponseStream();
                    StreamReader reader = new StreamReader(datastream);
                    jsonString = reader.ReadToEnd();
                    reader.Close();
                    datastream.Close();
                    coditechLogging.LogMessage($"{args[3]} API Call Successfully.", LoggingComponent, TraceLevel.Info);
                }
            }
            catch (WebException webException)
            {
                coditechLogging.LogMessage(webException, LoggingComponent, TraceLevel.Error);
            }
            catch (Exception ex)
            {
                coditechLogging.LogMessage(ex, LoggingComponent, TraceLevel.Error);
            }
        }
    }
}

