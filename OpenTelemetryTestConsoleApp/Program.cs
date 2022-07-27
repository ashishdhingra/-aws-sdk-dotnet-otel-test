using System.Diagnostics.Metrics;
using Amazon.DynamoDBv2;
using Amazon.S3;

// Define some important constants to initialize tracing with
var serviceName = "MyCompany.MyProduct.MyService";
var serviceVersion = "1.0.0";
using var meter = new Meter("TestMeter");

// Configure important OpenTelemetry settings and the console exporter
using var tracerProvider = OpenTelemetryTestConsoleApp.OpenTelemetryHelper.ConfigureTracerProvider(serviceName, serviceVersion);
using var meterProvider = OpenTelemetryTestConsoleApp.OpenTelemetryHelper.ConfigureMetricProvider(serviceName, serviceVersion, new List<Meter> { meter });

var s3Client = new AmazonS3Client();
_ = await s3Client.ListBucketsAsync();

var dynamoDbClient = new AmazonDynamoDBClient();
_ = await dynamoDbClient.ListTablesAsync();

// Ideally AWS SDK should have some mechanism to hook measurement of counters
Counter<int> listTablesCounter = meter.CreateCounter<int>("DynamoDB.ListTables", "request(s)", "A count of number of requests");
listTablesCounter?.Add(1);
Task.Delay(100).Wait();
listTablesCounter?.Add(1);
Task.Delay(100).Wait();
listTablesCounter?.Add(1);
Task.Delay(100).Wait();