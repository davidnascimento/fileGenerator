# File Generator
Project for file generation based on text extraction.

# Prerequisites
* The minimum platform versions is .NET Core 2.1.

Stack
* dotnet core 2.1
* Puppeteer Sharp
* Polly

- The Puppeteer library was used for providing the ability to use javascript commands on the web pages and to be headless browser. The Puppeteer library downloads a browser to crawler. So the first time you run the application, it may take a while
- The choice of the Polly library was due to the ease and power of expressing fallback policy simply and clearly.

# Usage

* dotnet run --project ./fileGeneratorApp/FileGeneratorApp.csproj

- The application accepts as parameter the size of the file to be generated as well as the buffer size. Must write file path.
- If not informed the application defaults
  File size 100Mb
  Buffer Size 1Mb
  Parameters if specified must be positive

# Test

 * dotnet test
