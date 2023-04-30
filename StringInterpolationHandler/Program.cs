// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Logging;

var logger = new Logger(LogLevel.Information);

var firstName = "Moaid";
var lastName = "Hathot";
var location = "127.0.0.1";
var address = "moaid@hathot.com";
var workId = 10;

logger.Log(LogLevel.Information, $"{firstName} {lastName} - {location} - {address} - {(true, workId)}");

