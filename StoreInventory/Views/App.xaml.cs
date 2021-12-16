using Microsoft.Extensions.DependencyInjection;
using Ninject;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RazorEngine.Text;
using StoreInventory.Services.MessageService;
using StoreInventory.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StoreInventory.Views
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMessageService>(new MessageService());
            services.AddSingleton<MainWindow>();

            //TemplateServiceConfiguration templateConfig = new TemplateServiceConfiguration();
            //templateConfig.EncodedStringFactory = new RawStringFactory();
            //var service = RazorEngineService.Create(templateConfig);
            //Engine.Razor = service;
        }

            private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}



 

        