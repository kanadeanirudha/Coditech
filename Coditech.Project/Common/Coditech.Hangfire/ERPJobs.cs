using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.Logger;
using Hangfire;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;

namespace Coditech.Hangfire
{
    public class ERPJobs : IERPJobs
    {
        protected readonly ICoditechLogging _coditechLogging;
        protected readonly IServiceProvider _serviceProvider;
        public ERPJobs(ICoditechLogging coditechLogging, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _coditechLogging = coditechLogging;
        }
        public virtual bool ConfigureJobs(TaskSchedulerModel model, out string hangfireJobId, ICoditechLogging coditechLogging)
        {
            var status = false;
            hangfireJobId = string.Empty;
            try
            {
                _coditechLogging.LogMessage("Hangfire job creation starting for " + model.SchedulerName, CoditechLoggingEnum.Components.ERP.ToString(), TraceLevel.Info);
                model.SchedulerClassName = $"Coditech{model.SchedulerCallFor}Helper";
                //In case of update, delete the current job and create a new job in Hangfire after that.
                bool removeJobResult = RemoveJob(model);

                if (model.IsInstantJob)
                {
                    hangfireJobId = BackgroundJob.Enqueue(() => this.Invoke(model.SchedulerName, model.SchedulerClassName, JsonConvert.SerializeObject(model)));
                }
                else if (model.SchedulerFrequency == APIConstant.OneTime)
                {
                    hangfireJobId = BackgroundJob.Schedule(() => this.Invoke(model.SchedulerName, model.SchedulerClassName, JsonConvert.SerializeObject(model)), new DateTimeOffset(model.StartDate.Value));
                }
                else if (model.SchedulerFrequency == APIConstant.Recurring)
                {
                    RecurringJob.AddOrUpdate(model.SchedulerClassName, () => Invoke(model.SchedulerName, model.SchedulerClassName, JsonConvert.SerializeObject(model)), model.CronExpression, TimeZoneInfo.Local);
                }
                _coditechLogging.LogMessage("Hangfire job created for " + model.SchedulerName, CoditechLoggingEnum.Components.ERP.ToString(), TraceLevel.Info);
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
                _coditechLogging.LogMessage($"Error in Hangfire :{ex.Message}", CoditechLoggingEnum.Components.ERP.ToString(), TraceLevel.Error, ex);
            }
            return status;
        }

        public virtual bool RemoveJob(TaskSchedulerModel schedulerModel)
        {
            bool result = true;

            try
            {
                if (schedulerModel.TaskSchedulerMasterId <= 0)
                    return result;

                //Delete one-time job
                if (!string.IsNullOrEmpty(schedulerModel.HangfireJobId))
                    result = BackgroundJob.Delete(schedulerModel.HangfireJobId);

                //Delete recurring job
                result = RemoveJob(schedulerModel.SchedulerCallFor);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.ERP.ToString(), TraceLevel.Error);
                result = false;
            }

            return result;
        }

        public virtual bool RemoveJob(List<TaskSchedulerModel> schedulerModelList)
        {
            bool result = true;
            try
            {
                foreach (TaskSchedulerModel schedulerModel in schedulerModelList)
                {
                    if (schedulerModel.TaskSchedulerMasterId <= 0)
                        return result;

                    //Delete one-time job
                    if (!string.IsNullOrEmpty(schedulerModel.HangfireJobId))
                        result = BackgroundJob.Delete(schedulerModel.HangfireJobId);

                    //Delete recurring job
                    result = RemoveJob(schedulerModel.SchedulerCallFor);
                }
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage(ex, CoditechLoggingEnum.Components.ERP.ToString(), TraceLevel.Error);
                result = false;
            }

            return result;
        }

        public virtual bool RemoveJobs(List<string> schedulerNamesList)
        {
            bool status = false;
            try
            {
                foreach (string schedulerName in schedulerNamesList)
                {
                    status = RemoveJob(schedulerName);
                }
            }
            catch (Exception ex)
            {
                status = false;
                _coditechLogging.LogMessage($"Error in removing Hangfire job:{ex.Message}", CoditechLoggingEnum.Components.ERP.ToString(), TraceLevel.Error, ex);
            }
            return status;
        }

        [DisplayName("{0}")]
        public virtual void Invoke(string SchedulerName, string SchedulerClassName, string param)
        {
            TaskSchedulerModel obj = JsonConvert.DeserializeObject<TaskSchedulerModel>(param);
            string type = Convert.ToString(obj.SchedulerClassName);
            ISchedulerProviders _provider = GetSchedulerProviderObject(SchedulerClassName);

            ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;

            try
            {
                _coditechLogging.LogMessage("param for execution :" + param, CoditechLoggingEnum.Components.ERP.ToString(), TraceLevel.Info);
                _coditechLogging.LogMessage("Start invoking Hangfire job for " + SchedulerName, CoditechLoggingEnum.Components.ERP.ToString(), TraceLevel.Info);
                if (!Equals(_provider, null))
                    _provider.InvokeMethod(obj);
                _coditechLogging.LogMessage("Hangfire job invoked for " + SchedulerName, CoditechLoggingEnum.Components.ERP.ToString(), TraceLevel.Info);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage($"Error in invoking Hangfire job:{ex.Message}", CoditechLoggingEnum.Components.ERP.ToString(), TraceLevel.Error, ex);
            }
        }

        protected virtual ISchedulerProviders GetSchedulerProviderObject(string schedulerType)
        {
            if (!string.IsNullOrEmpty(schedulerType))
            {
                StructureMap.Container container = new StructureMap.Container(content =>
                content.Scan(scan =>
                {
                    scan.AssemblyContainingType<ISchedulerProviders>();
                    scan.AddAllTypesOf<ISchedulerProviders>();
                }));
                string schedulerClassName = container.Model.AllInstances.FirstOrDefault(x => x.Description.Contains(schedulerType)).Name;
                return !string.IsNullOrEmpty(schedulerType) ? container.With<IServiceProvider>(_serviceProvider).GetInstance<ISchedulerProviders>(schedulerClassName) : null;
            }
            return null;
        }

        protected virtual bool RemoveJob(string schedulerName)
        {
            bool status;
            try
            {
                RecurringJob.RemoveIfExists($"Coditech{schedulerName}Helper");
                _coditechLogging.LogMessage("Hangfire job Removed for " + schedulerName, CoditechLoggingEnum.Components.ERP.ToString(), TraceLevel.Info);
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
                _coditechLogging.LogMessage($"Error in removing Hangfire job:{ex.Message}", CoditechLoggingEnum.Components.ERP.ToString(), TraceLevel.Error, ex);
            }
            return status;
        }
    }
}