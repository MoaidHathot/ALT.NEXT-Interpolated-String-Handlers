// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Logging;

var logger = new Logger(LogLevel.Information);

var firstName = "Moaid";
var lastName = "Hathot";

logger.Log(LogLevel.Information, $"{firstName} {lastName}");


