using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Tracing;
using Common.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ITraceWriter = System.Web.Http.Tracing.ITraceWriter;

namespace SVB.Ticker.Server.Web
{
  public class WebApiConfiguration
  {
    public static void Configure(HttpConfiguration config, bool isTraceMode, string allowedOrigins, string allowedHeaders, string allowedMethods)
    {
      config.EnableCors(new CustomCorsPolicyProvider(allowedHeaders, allowedHeaders, allowedMethods));

      if (isTraceMode)
      {
        config.EnableSystemDiagnosticsTracing();
        config.Services.Replace(typeof(ITraceWriter), new CommonLoggingTraceWriter());
      }

      config.MapHttpAttributeRoutes();

      // Only JSON serialization, please
      config.Formatters.Remove(config.Formatters.XmlFormatter);

      JsonMediaTypeFormatter jsonFormatter = config.Formatters.JsonFormatter;
      // We want Json.Net to be used
      jsonFormatter.UseDataContractJsonSerializer = false;

      JsonSerializerSettings jsonSerializerSettings = jsonFormatter.SerializerSettings;
      // We want camelCased properties
      jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
      // Ignore reference loops (e.g. candidate -> application -> job -> application -> candidate -> ...)
      jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
      // We want DateTimeOffsets instead of DateTimes
      jsonSerializerSettings.DateParseHandling = DateParseHandling.DateTimeOffset;
      // We assume that dates and date-times are transferred using the ISO-8601 format
      jsonSerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
      // Handles timezones always as UTC
      jsonSerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
    }

    private class CommonLoggingTraceWriter : ITraceWriter
    {
      private static readonly ILog Log = LogManager.GetLogger<CommonLoggingTraceWriter>();

      #region Implementation of ITraceWriter

      /// <summary>
      /// Invokes the specified traceAction to allow setting values in a new <see cref="T:System.Web.Http.Tracing.TraceRecord"/> if and only if tracing is permitted at the given category and level.
      /// </summary>
      /// <param name="request">The current <see cref="T:System.Net.Http.HttpRequestMessage"/>.   It may be null but doing so will prevent subsequent trace analysis  from correlating the trace to a particular request.</param><param name="category">The logical category for the trace.  Users can define their own.</param><param name="level">The <see cref="T:System.Web.Http.Tracing.TraceLevel"/> at which to write this trace.</param><param name="traceAction">The action to invoke if tracing is enabled.  The caller is expected to fill in the fields of the given <see cref="T:System.Web.Http.Tracing.TraceRecord"/> in this action.</param>
      public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
      {
        TraceRecord record = new TraceRecord(request, category, level);
        traceAction(record);
        WriteTrace(record);
      }

      private static void WriteTrace(TraceRecord traceRecord)
      {
        const string traceFormat =
          "RequestId={1};{0}Kind={2};{0}Status={3};{0}Operation={4};{0}Operator={5};{0}Category={6};{0}Request={7};{0}Message={8}";

        var args = new object[]
        {
          Environment.NewLine,
          traceRecord.RequestId,
          traceRecord.Kind,
          traceRecord.Status,
          traceRecord.Operation,
          traceRecord.Operator,
          traceRecord.Category,
          traceRecord.Request,
          traceRecord.Message
        };

        switch (traceRecord.Level)
        {
          case TraceLevel.Debug:
            Log.Debug(x => x(traceFormat, args));
            break;

          case TraceLevel.Info:
            Log.Info(x => x(traceFormat, args));
            break;

          case TraceLevel.Warn:
            Log.Warn(x => x(traceFormat, args));
            break;

          case TraceLevel.Error:
            Log.Error(x => x(traceFormat, args));
            break;

          case TraceLevel.Fatal:
            Log.Fatal(x => x(traceFormat, args));
            break;
        }
      }

      #endregion
    }
  }
}