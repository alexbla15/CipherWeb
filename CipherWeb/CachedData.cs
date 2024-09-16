﻿using CipherData.Models;

namespace CipherWeb
{
    public static class CachedData
    {
        public static readonly BooleanCondition BooleanConditionExample = BooleanCondition.Empty();
        public static readonly Category CategoryExample = Category.Empty();
        public static readonly CategoryProperty CategoryPropertyExample = CategoryProperty.Empty();
        public static readonly CreateRelocationEvent CreateRelocationEventExample = CreateRelocationEvent.Empty();
        public static readonly CreateTranserAmountEvent CreateTranserAmountEventExample = CreateTranserAmountEvent.Empty();
        public static readonly Event EventExample = Event.Random();
        public static readonly Package PackageExample = Package.Empty();
        public static readonly PackageProperty PackagePropertyExample = new();
        public static readonly Process ProcessExample = Process.Random();
        public static readonly ProcessDefinition ProcessDefinitionExample = ProcessDefinition.Random();
        public static readonly ProcessStepDefinition ProcessStepDefinitionExample = ProcessStepDefinition.Random();
        public static readonly StorageSystem SystemExample = StorageSystem.Empty();
        public static readonly Vessel VesselExample = Vessel.Empty();
        public static readonly Unit UnitExample = Unit.Empty();
        public static readonly UserAction UserActionExample = UserAction.Empty();

        public static readonly Tuple<List<Category>, ErrorResponse> AllCategories = Category.All();
        public static readonly Tuple<List<Event>, ErrorResponse> AllEvents = Event.All();
        public static readonly Tuple<List<Package>, ErrorResponse> AllPackages = Package.All();
        public static readonly Tuple<List<Process>, ErrorResponse> AllProcesses = Process.All();
        public static readonly Tuple<List<ProcessDefinition>, ErrorResponse> AllProcessDefinitions = ProcessDefinition.All();
        public static readonly Tuple<List<StorageSystem>, ErrorResponse> AllSystems = StorageSystem.All();
        public static readonly Tuple<List<Vessel>, ErrorResponse> AllVessels = Vessel.All();
        public static readonly Tuple<List<Unit>, ErrorResponse> AllUnits = Unit.All();
    }
}
