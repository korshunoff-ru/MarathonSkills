using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MarathonSkills
{
    /// <summary>
    /// Bootstrapper
    /// </summary>
    public static class BootStrapper
    {

        #region Private Member

        private static ILifetimeScope _rootScope;
        private static MainViewModel _mainViewModel;

        #endregion

        #region Public Member

        public static MainViewModel RootVisual
        {
            get
            {
                if (_rootScope == null)
                {
                    Start();
                }

                _mainViewModel = _rootScope.Resolve<MainViewModel>();
                return _mainViewModel;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Starts the bootstrapper
        /// </summary>
        public static void Start()
        {
            if (_rootScope != null)
            {
                return;
            }
            var builder = new ContainerBuilder();

            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .SingleInstance()
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("ViewModel"));

            _rootScope = builder.Build();
        }

        /// <summary>
        /// Stops the bootstrapper
        /// </summary>
        public static void Stop()
        {
            _rootScope.Dispose();
        }

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            if (_rootScope == null)
            {
                throw new Exception("Bootstrapper hasn't been started!");
            }

            return _rootScope.Resolve<T>(new Parameter[0]);
        }

        public static T Resolve<T>(Parameter[] parameters)
        {
            if (_rootScope == null)
            {
                throw new Exception("Bootstrapper hasn't been started!");
            }

            return _rootScope.Resolve<T>(parameters);
        }

        #endregion


    }
}
