using Core;
using PortfolioRealtime.Logger;

Logger.Initialize();

var server = new Server(6132);
await server.Start();