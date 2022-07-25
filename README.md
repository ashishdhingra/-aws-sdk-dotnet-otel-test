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
- Navigate to the root of OpenTelemetryTestConsoleApp .NET project (`OpenTelemetryTestConsoleApp.csproj`) file.
- Execute command `dotnet restore` to restore NuGet package.
- Execute command `dotnet build --configuration Release` to build the project in `Release` configuration.
- Navigate to directory `bin/Release/net6.0`.
- Execute command `dotnet OpenTelemetryTestConsoleApp.dll` to run the demo application.

This produces the following output:

```console
Activity.TraceId:          a20bd93d93281e6e725846d0185bfc78
Activity.SpanId:           89794b6d59ba4665
Activity.TraceFlags:           Recorded
Activity.ActivitySourceName: Amazon.AWS.AWSClientInstrumentation
Activity.DisplayName: S3.ListBuckets
Activity.Kind:        Client
Activity.StartTime:   2022-07-25T23:40:48.7665680Z
Activity.Duration:    00:00:03.6271640
Activity.Tags:
    aws.service: S3
    aws.operation: ListBuckets
    aws.region: us-east-2
    aws.requestId: EBD70Y5PABDCVXX2
    http.status_code: 200
    http.response_content_length: 0
Resource associated with Activity:
    service.name: MyCompany.MyProduct.MyService
    service.version: 1.0.0
    service.instance.id: 6e23c968-d924-4480-90a2-4e623a1ced23

Activity.TraceId:          74da628613d7627e664d841a4e63524a
Activity.SpanId:           20183b5dbf0cc5a4
Activity.TraceFlags:           Recorded
Activity.ActivitySourceName: Amazon.AWS.AWSClientInstrumentation
Activity.DisplayName: DynamoDBv2.ListTables
Activity.Kind:        Client
Activity.StartTime:   2022-07-25T23:40:52.4184170Z
Activity.Duration:    00:00:00.3135650
Activity.Tags:
    aws.service: DynamoDBv2
    aws.operation: ListTables
    aws.region: us-east-2
    aws.requestId: U3IHGUEFDOO23SH0592LQUD9KVVV4KQNSO5AEMVJF66Q9ASUAAJG
    http.status_code: 200
    http.response_content_length: 28
Resource associated with Activity:
    service.name: MyCompany.MyProduct.MyService
    service.version: 1.0.0
    service.instance.id: 6e23c968-d924-4480-90a2-4e623a1ced23
```

## References
- [OpenTelemetry .NET](https://github.com/open-telemetry/opentelemetry-dotnet)
- [Getting Started with the .NET SDK on Traces Instrumentation](https://aws-otel.github.io/docs/getting-started/dotnet-sdk)
- [opentelemetry-dotnet-contrib/OpenTelemetry.Contrib.Instrumentation.AWS](https://github.com/open-telemetry/opentelemetry-dotnet-contrib/tree/main/src/OpenTelemetry.Contrib.Instrumentation.AWS)
- [opentelemetry-dotnet-contrib](https://github.com/open-telemetry/opentelemetry-dotnet-contrib)
