using CipherData.Models;
using CipherWeb.Data;
using CipherWeb.Shared.Components;
using Radzen;
using System;
using System.Reflection;

namespace CipherWeb
{
    public class CachedData
    {
        public static Category CategoryExample = Category.Random();
        public static Event EventExample = Event.Random();
        public static Package PackageExample = Package.Random();
        public static Process ProcessExample = Process.Random();
        public static ProcessDefinition ProcessDefinitionExample = ProcessDefinition.Random();
        public static StorageSystem SystemExample = StorageSystem.Random();
        public static Vessel VesselExample = Vessel.Random();

        public static Tuple<List<Category>, ErrorResponse> AllCategories = Category.All();
        public static Tuple<List<Event>, ErrorResponse> AllEvents = Event.All();
        public static Tuple<List<Package>, ErrorResponse> AllPackages = Package.All();
        public static Tuple<List<Process>, ErrorResponse> AllProcesses = Process.All();
        public static Tuple<List<ProcessDefinition>, ErrorResponse> AllProcessDefinitions = ProcessDefinition.All();
        public static Tuple<List<StorageSystem>, ErrorResponse> AllSystems = StorageSystem.All();
        public static Tuple<List<Vessel>, ErrorResponse> AllVessels = Vessel.All();
    }
}
