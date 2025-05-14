using DFrame;

var portWeb = 7312;
var portListenWorker = 7313;
var useHttps = false;

var builder = DFrameApp.CreateBuilder(portWeb, portListenWorker, useHttps);
await builder.RunControllerAsync();