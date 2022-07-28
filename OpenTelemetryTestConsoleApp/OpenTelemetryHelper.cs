using System.Diagnostics.Metrics;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace OpenTelemetryTestConsoleApp
{
    public static class OpenTelemetryHelper
	{
		public static TracerProvider ConfigureTracerProvider(string serviceName, string serviceVersion)
        {
            var zipkinUri = "http://localhost:9411/api/v2/spans";
            var tracerProvider = Sdk.CreateTracerProviderBuilder()
                .AddSource(serviceName)
                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(serviceName: serviceName, serviceVersion: serviceVersion))
                .AddAWSInstrumentation()
                .AddConsoleExporter()
                .AddZipkinExporter(o =>
                {
                    o.Endpoint = new Uri(zipkinUri);
                })
                .Build();

            return tracerProvider;
        }

        public static MeterProvider? ConfigureMetricProvider(string serviceName, string serviceVersion, IList<Meter> meters)
        {
            MeterProvider? meterProvider = null;
            if (meters != null && meters.Count > 0) {
                var providerBuilder = Sdk.CreateMeterProviderBuilder()
                    .SetResourceBuilder(
                        ResourceBuilder.CreateDefault()
                            .AddService(serviceName: serviceName, serviceVersion: serviceVersion))
                    .AddConsoleExporter((exporterOptions, metricReaderOptions) =>
                    {
                        exporterOptions.Targets = ConsoleExporterOutputTargets.Console;
                        metricReaderOptions.PeriodicExportingMetricReaderOptions.ExportIntervalMilliseconds = 100;
                    });

                foreach (var meter in meters)
                {
                    providerBuilder.AddMeter(meter.Name); // All instruments from this meter are enabled.
                }

                meterProvider = providerBuilder.Build();
            }

            return meterProvider;
        }
	}
}

