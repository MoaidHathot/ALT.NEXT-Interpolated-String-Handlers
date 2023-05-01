using Dumpify;
using Microsoft.Extensions.Logging;

var logger = new Logger(Microsoft.Extensions.Logging.LogLevel.Information);

var name = "example@example.com";
var age = 34;

logger.Log(LogLevel.Information, $"{name}  age: {(true, "sdfdsf")}");


