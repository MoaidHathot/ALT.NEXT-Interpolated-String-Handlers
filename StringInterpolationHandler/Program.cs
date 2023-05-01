using Dumpify;
using Microsoft.Extensions.Logging;

var logger = new Logger(Microsoft.Extensions.Logging.LogLevel.Information);

var firstName = "Moaid";
var lastName = "Hathot";

var email = "moaid.hathot@outlook.com";

logger.Log(LogLevel.Information, $"FirstName: '{firstName}', lastName: '{lastName}', email: '{email}'");

