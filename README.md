# aws-sdk-dotnet-otel-test

This sample shows how to use [OpenTelemetry.Contrib.Instrumentation.AWS](https://www.nuget.org/packages/OpenTelemetry.Contrib.Instrumentation.AWS) package (GitHub repository [opentelemetry-dotnet-contrib/OpenTelemetry.Contrib.Instrumentation.AWS](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/tree/main/src/OpenTelemetry.Contrib.Instrumentation.AWS)) to instrument the following calls in AWS SDK for .NET:
- `ListBucketsAsync` call on S3 client.
- `ListTablesAsync` call on DynamoDB client.

The example exports spans data to `Console`.

## Prerequisites

Complete the following tasks:

- Install **.NET 6 SDK** by following these steps at https://dotnet.microsoft.com/en-us/download/dotnet/6.0.
- If you don't have an AWS account, [create one](https://aws.amazon.com/premiumsupport/knowledge-center/create-and-activate-aws-account/).
  - If you're an Amazon employee, see the internal wiki for creating an AWS account.
- Set up [AWS Shared Credential File](https://docs.aws.amazon.com/cli/latest/userguide/cli-configure-files.html).
  - Your `~/.aws/credentials` (`%UserProfile%\.aws\credentials` on Windows) should look like the following:
    ```
    [default]
    aws_access_key_id = <ACCESS_KEY>
    aws_secret_access_key = <SECRET_ACCESS_KEY>
    ```
  - Your `~/.aws/config` (`%UserProfile%\.aws\config` on Windows) should look like the following:
    ```
    [default]
    region = us-east-2
    ```

## Setup

The test code from this package uses `AddAWSInstrumentation()` extension method for `TracerProviderBuilder` from [OpenTelemetry.Contrib.Instrumentation.AWS](https://www.nuget.org/packages/OpenTelemetry.Contrib.Instrumentation.AWS) package. Since this is a Console application for demonstration purposes, internally it relies on .NET's `Activity` types available in `System.Diagnostics` namespace (specifically `ActivitySource` and `Activity` types).

Following steps could be used for demonstration purposes:
- Clone the repository.
- Navigate to the folder where repository was cloned.
- Execute command `dotnet run --project ./OpenTelemetryTestConsoleApp/OpenTelemetryTestConsoleApp.csproj --configuration Release` to run the project in `Release` configuration.

This produces the following output:

```console
Resource associated with Metric:
    service.name: MyCompany.MyProduct.MyService
    service.version: 1.0.0
    service.instance.id: a191b4c8-9d1a-4182-b423-69ed200c45aa
Activity.TraceId:          5c22f3104e1214f8058fc917ea1104c1
Activity.SpanId:           a3761faa2093d391
Activity.TraceFlags:           Recorded
Activity.ActivitySourceName: Amazon.AWS.AWSClientInstrumentation
Activity.DisplayName: S3.ListBuckets
Activity.Kind:        Client
Activity.StartTime:   2022-07-27T21:34:41.7679270Z
Activity.Duration:    00:00:03.6765920
Activity.Tags:
    aws.service: S3
    aws.operation: ListBuckets
    aws.region: us-east-2
    aws.requestId: 3CNM9B8AHQFZBED1
    http.status_code: 200
    http.response_content_length: 0
Resource associated with Activity:
    service.name: MyCompany.MyProduct.MyService
    service.version: 1.0.0
    service.instance.id: 6be77db6-7af9-4bb1-b2f5-a348bdcd37c3

Activity.TraceId:          3556a9f0f348920950c159afa16f4c9b
Activity.SpanId:           89c0321f4878de0d
Activity.TraceFlags:           Recorded
Activity.ActivitySourceName: Amazon.AWS.AWSClientInstrumentation
Activity.DisplayName: DynamoDBv2.ListTables
Activity.Kind:        Client
Activity.StartTime:   2022-07-27T21:34:45.4569510Z
Activity.Duration:    00:00:00.3085510
Activity.Tags:
    aws.service: DynamoDBv2
    aws.operation: ListTables
    aws.region: us-east-2
    aws.requestId: FAJPNUMLFSP5D2KJI5G5EP6FUVVV4KQNSO5AEMVJF66Q9ASUAAJG
    http.status_code: 200
    http.response_content_length: 17
Resource associated with Activity:
    service.name: MyCompany.MyProduct.MyService
    service.version: 1.0.0
    service.instance.id: 6be77db6-7af9-4bb1-b2f5-a348bdcd37c3


Export DynamoDB.ListTables, A count of number of requests, Unit: request(s), Meter: TestMeter
(2022-07-27T21:34:41.5044230Z, 2022-07-27T21:34:45.8397100Z] LongSum
Value: 1

Export DynamoDB.ListTables, A count of number of requests, Unit: request(s), Meter: TestMeter
(2022-07-27T21:34:41.5044230Z, 2022-07-27T21:34:45.9364000Z] LongSum
Value: 2

Export DynamoDB.ListTables, A count of number of requests, Unit: request(s), Meter: TestMeter
(2022-07-27T21:34:41.5044230Z, 2022-07-27T21:34:46.0361970Z] LongSum
Value: 3
```
**ZipKins trace example:**
![ZipKins example](images/zipkins_exporter_example.png?raw=true)

## References
- [OpenTelemetry .NET](https://github.com/open-telemetry/opentelemetry-dotnet)
- [Getting Started with the .NET SDK on Traces Instrumentation](https://aws-otel.github.io/docs/getting-started/dotnet-sdk)
- [opentelemetry-dotnet-contrib/OpenTelemetry.Contrib.Instrumentation.AWS](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/tree/main/src/OpenTelemetry.Contrib.Instrumentation.AWS)
- [opentelemetry-dotnet-contrib](https://github.com/open-telemetry/opentelemetry-dotnet-contrib)
