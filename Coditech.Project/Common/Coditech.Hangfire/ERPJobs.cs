using Coditech.Common.API;
using Coditech.Common.API.Model;
using Coditech.Common.Logger;
using Hangfire;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using Microsoft.Extensions.DependencyInjection;

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
            TaskSchedulerModel obj = null;
            try
            {
                obj = JsonConvert.DeserializeObject<TaskSchedulerModel>(param);
            }
            catch (Exception ex)
            {
                _coditechLogging.LogMessage($"Failed to deserialize scheduler parameter. Param: {param}. Error: {ex.Message}", CoditechLoggingEnum.Components.ERP.ToString(), TraceLevel.Error, ex);
            }

            // Try to resolve provider using the provided SchedulerClassName first.
            ISchedulerProviders _provider = GetSchedulerProviderObject(SchedulerClassName);

            // Fallback: if provider not found and deserialized object contains a class name, try that too.
            if (_provider == null && obj != null && !string.IsNullOrEmpty(obj.SchedulerClassName))
            {
                _provider = GetSchedulerProviderObject(obj.SchedulerClassName);
            }

            // Allow self-signed certificates for outgoing calls initiated by jobs.
            ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;

            try
            {
                _coditechLogging.LogMessage("param for execution :" + param, CoditechLoggingEnum.Components.ERP.ToString(), TraceLevel.Info);
                _coditechLogging.LogMessage("Start invoking Hangfire job for " + SchedulerName, CoditechLoggingEnum.Components.ERP.ToString(), TraceLevel.Info);

                if (_provider == null)
                {
                    _coditechLogging.LogMessage($"No scheduler provider found for '{SchedulerClassName}' or payload value. Job will not be invoked.", CoditechLoggingEnum.Components.ERP.ToString(), TraceLevel.Warning);
                    return;
                }

                if (obj == null)
                {
                    _coditechLogging.LogMessage("Scheduler payload is null or invalid. Invocation aborted.", CoditechLoggingEnum.Components.ERP.ToString(), TraceLevel.Warning);
                    return;
                }

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
            if (string.IsNullOrEmpty(schedulerType))
                return null;

            // Normalize schedulerType for matching
            var normalized = schedulerType.Trim();
            // If caller passed a helper class name like "CoditechXyzHelper", try to extract "Xyz" as well
            if (normalized.StartsWith("Coditech") && normalized.EndsWith("Helper"))
            {
                normalized = normalized.Substring("Coditech".Length, normalized.Length - "Coditech".Length - "Helper".Length);
            }

            // Find concrete types implementing ISchedulerProviders in loaded assemblies
            var providerType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a =>
                {
                    try { return a.GetTypes(); }
                    catch { return Array.Empty<Type>(); }
                })
                .Where(t => typeof(ISchedulerProviders).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .FirstOrDefault(t =>
                {
                    try
                    {
                        // Try to get an instance from DI or create one with ActivatorUtilities to allow constructor injection
                        object instance = _serviceProvider.GetService(t) ?? ActivatorUtilities.CreateInstance(_serviceProvider, t);
                        var descProp = t.GetProperty("Description");
                        if (descProp != null)
                        {
                            var val = descProp.GetValue(instance)?.ToString();
                            if (!string.IsNullOrEmpty(val) && val.Contains(normalized, StringComparison.OrdinalIgnoreCase))
                                return true;
                        }

                        // Fall back to matching against the type name
                        if (t.Name.Contains(normalized, StringComparison.OrdinalIgnoreCase) || t.FullName?.Contains(normalized, StringComparison.OrdinalIgnoreCase) == true)
                            return true;
                    }
                    catch { }
                    return false;
                });

            if (providerType == null)
                return null;

            // Resolve from DI if possible, otherwise create via ActivatorUtilities to allow constructor injection
            var providerInstance = _serviceProvider.GetService(providerType) ?? ActivatorUtilities.CreateInstance(_serviceProvider, providerType);
            return providerInstance as ISchedulerProviders;
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