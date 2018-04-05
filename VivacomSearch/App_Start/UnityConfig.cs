using API.Entities;
using API.Implementations;
using API.Interfaces;
using System;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace VivacomSearch
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IFileOperations, FileOperations>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}