﻿using CipherData.Models;
using CipherData;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.VisualBasic;
using CipherData.Requests;

namespace CipherData
{
    public static class RandomData
    {
        public static readonly CustomObjectBooleanCondition RandomCustomObjectBooleanCondition = CustomObjectBooleanCondition.Random();
        public static readonly GroupedBooleanCondition RandomGroupedBooleanCondition = GroupedBooleanCondition.Random();
        public static readonly UserActionResponse RandomUserActionResponse = UserActionResponse.Random();

        public static readonly Category RandomCategory = Category.Random();
        public static readonly Event RandomEvent = Event.Random();
        public static readonly Package RandomPackage = Package.Random();
        public static readonly Process RandomProcess = Process.Random();
        public static readonly ProcessDefinition RandomProcessDefinition = ProcessDefinition.Random();
        public static readonly StorageSystem RandomSystem = StorageSystem.Random();
        public static readonly Unit RandomUnit = Unit.Random();
        public static readonly Vessel RandomVessel = Vessel.Random();

        public static readonly List<Category> RandomCategories = RandomFuncs.FillRandomObjects(20, Category.Random);
        public static readonly List<Event> RandomEvents = RandomFuncs.FillRandomObjects(20, Event.Random);
        public static readonly List<Package> RandomPackages = RandomFuncs.FillRandomObjects(20, Package.Random);
        public static readonly List<Process> RandomProcesses = RandomFuncs.FillRandomObjects(20, Process.Random);
        public static readonly List<ProcessDefinition> RandomProcessDefinitions = RandomFuncs.FillRandomObjects(20, ProcessDefinition.Random);
        public static readonly List<StorageSystem> RandomSystems = RandomFuncs.FillRandomObjects(20, StorageSystem.Random);
        public static readonly List<Vessel> RandomVessels = RandomFuncs.FillRandomObjects(20, Vessel.Random);
    }
}
