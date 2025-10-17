using CallWach.API.Controller;
using CallWatch.Application;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();

serviceCollection.AddTransient<CallWathController>();
serviceCollection.AddApplication();
var serviceProvider = serviceCollection.BuildServiceProvider();
var controller = serviceProvider.GetService<CallWathController>();
controller?.Execute();