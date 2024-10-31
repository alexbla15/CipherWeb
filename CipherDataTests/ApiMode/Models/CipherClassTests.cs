using CipherData.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CipherData.ApiMode.Tests
{
    [TestClass()]
    public class CipherClassTests
    {
        public IPackage p = new Package()
        {
            BrutMass = 5,
            NetMass = 4,
            CreatedAt = DateTime.Parse("1/1/2024"),
            Id = "111",
            Category = new Category() { Id = "222" },
            Children = new() { new Package() { Id = "333" }, new Package() { Id = "444" } },
            Description = "555",
            Parent = new Package() { Id = "666" },
            Vessel = new Vessel() { Id = "777" },
            System = new StorageSystem() { Id = "888" },
            Uuid = 10203040,
            DestinationProcesses = new() { new ProcessDefinition() { Id = "999" } },
            Properties = new() { new PackageProperty() { Name="AAA", Value="BBB"} }
        };

        [TestMethod()]
        public void ToJsonTest()
        {
            string func_res = p.ToJson();
            string real_res = "{\r\n  \"Description\": \"555\",\r\n  \"Properties\": [\r\n    {\r\n      \"Name\": \"AAA\",\r\n      \"Value\": \"BBB\"\r\n    }\r\n  ],\r\n  \"Vessel\": {\r\n    \"Name\": null,\r\n    \"Type\": \"\",\r\n    \"ContainingPackages\": null,\r\n    \"System\": null,\r\n    \"Id\": \"777\",\r\n    \"ClearenceLevel\": \"\",\r\n    \"Uuid\": 0\r\n  },\r\n  \"System\": {\r\n    \"Name\": \"\",\r\n    \"Description\": \"\",\r\n    \"Properties\": null,\r\n    \"Parent\": null,\r\n    \"Children\": null,\r\n    \"Unit\": null,\r\n    \"Id\": \"888\",\r\n    \"ClearenceLevel\": \"\",\r\n    \"Uuid\": 0\r\n  },\r\n  \"BrutMass\": 5,\r\n  \"NetMass\": 4,\r\n  \"CreatedAt\": \"2024-01-01 00:00\",\r\n  \"Parent\": {\r\n    \"Description\": null,\r\n    \"Properties\": null,\r\n    \"Vessel\": null,\r\n    \"System\": {\r\n      \"Name\": \"\",\r\n      \"Description\": \"\",\r\n      \"Properties\": null,\r\n      \"Parent\": null,\r\n      \"Children\": null,\r\n      \"Unit\": null,\r\n      \"Id\": \"\",\r\n      \"ClearenceLevel\": \"\",\r\n      \"Uuid\": 0\r\n    },\r\n    \"BrutMass\": 0,\r\n    \"NetMass\": 0,\r\n    \"CreatedAt\": \"0001-01-01 00:00\",\r\n    \"Parent\": null,\r\n    \"Children\": null,\r\n    \"Category\": {\r\n      \"Name\": \"\",\r\n      \"Description\": \"\",\r\n      \"IdMask\": [],\r\n      \"Properties\": null,\r\n      \"CreatingProcesses\": [],\r\n      \"ConsumingProcesses\": [],\r\n      \"MaterialType\": null,\r\n      \"Parent\": null,\r\n      \"Children\": null,\r\n      \"Id\": \"\",\r\n      \"ClearenceLevel\": \"\",\r\n      \"Uuid\": 0\r\n    },\r\n    \"DestinationProcesses\": [],\r\n    \"Concentration\": 0,\r\n    \"Id\": \"666\",\r\n    \"ClearenceLevel\": \"\",\r\n    \"Uuid\": 0\r\n  },\r\n  \"Children\": [\r\n    {\r\n      \"Description\": null,\r\n      \"Properties\": null,\r\n      \"Vessel\": null,\r\n      \"System\": {\r\n        \"Name\": \"\",\r\n        \"Description\": \"\",\r\n        \"Properties\": null,\r\n        \"Parent\": null,\r\n        \"Children\": null,\r\n        \"Unit\": null,\r\n        \"Id\": \"\",\r\n        \"ClearenceLevel\": \"\",\r\n        \"Uuid\": 0\r\n      },\r\n      \"BrutMass\": 0,\r\n      \"NetMass\": 0,\r\n      \"CreatedAt\": \"0001-01-01 00:00\",\r\n      \"Parent\": null,\r\n      \"Children\": null,\r\n      \"Category\": {\r\n        \"Name\": \"\",\r\n        \"Description\": \"\",\r\n        \"IdMask\": [],\r\n        \"Properties\": null,\r\n        \"CreatingProcesses\": [],\r\n        \"ConsumingProcesses\": [],\r\n        \"MaterialType\": null,\r\n        \"Parent\": null,\r\n        \"Children\": null,\r\n        \"Id\": \"\",\r\n        \"ClearenceLevel\": \"\",\r\n        \"Uuid\": 0\r\n      },\r\n      \"DestinationProcesses\": [],\r\n      \"Concentration\": 0,\r\n      \"Id\": \"333\",\r\n      \"ClearenceLevel\": \"\",\r\n      \"Uuid\": 0\r\n    },\r\n    {\r\n      \"Description\": null,\r\n      \"Properties\": null,\r\n      \"Vessel\": null,\r\n      \"System\": {\r\n        \"Name\": \"\",\r\n        \"Description\": \"\",\r\n        \"Properties\": null,\r\n        \"Parent\": null,\r\n        \"Children\": null,\r\n        \"Unit\": null,\r\n        \"Id\": \"\",\r\n        \"ClearenceLevel\": \"\",\r\n        \"Uuid\": 0\r\n      },\r\n      \"BrutMass\": 0,\r\n      \"NetMass\": 0,\r\n      \"CreatedAt\": \"0001-01-01 00:00\",\r\n      \"Parent\": null,\r\n      \"Children\": null,\r\n      \"Category\": {\r\n        \"Name\": \"\",\r\n        \"Description\": \"\",\r\n        \"IdMask\": [],\r\n        \"Properties\": null,\r\n        \"CreatingProcesses\": [],\r\n        \"ConsumingProcesses\": [],\r\n        \"MaterialType\": null,\r\n        \"Parent\": null,\r\n        \"Children\": null,\r\n        \"Id\": \"\",\r\n        \"ClearenceLevel\": \"\",\r\n        \"Uuid\": 0\r\n      },\r\n      \"DestinationProcesses\": [],\r\n      \"Concentration\": 0,\r\n      \"Id\": \"444\",\r\n      \"ClearenceLevel\": \"\",\r\n      \"Uuid\": 0\r\n    }\r\n  ],\r\n  \"Category\": {\r\n    \"Name\": \"\",\r\n    \"Description\": \"\",\r\n    \"IdMask\": [],\r\n    \"Properties\": null,\r\n    \"CreatingProcesses\": [],\r\n    \"ConsumingProcesses\": [],\r\n    \"MaterialType\": null,\r\n    \"Parent\": null,\r\n    \"Children\": null,\r\n    \"Id\": \"222\",\r\n    \"ClearenceLevel\": \"\",\r\n    \"Uuid\": 0\r\n  },\r\n  \"DestinationProcesses\": [\r\n    {\r\n      \"Name\": \"\",\r\n      \"Description\": \"\",\r\n      \"Steps\": [],\r\n      \"Id\": \"999\",\r\n      \"ClearenceLevel\": \"\",\r\n      \"Uuid\": 0\r\n    }\r\n  ],\r\n  \"Concentration\": 0.8,\r\n  \"Id\": \"111\",\r\n  \"ClearenceLevel\": \"\",\r\n  \"Uuid\": 10203040\r\n}";
            Assert.AreEqual(func_res, real_res);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            IPackage p2 = new Package()
            {
                BrutMass = 5,
                NetMass = 4,
                CreatedAt = DateTime.Parse("1/1/2024"),
                Id = "111",
                Category = new Category() { Id = "222" },
                Children = new() { new Package() { Id = "333" }, new Package() { Id = "444" } },
                Description = "555",
                Parent = new Package() { Id = "666" },
                Vessel = new Vessel() { Id = "777" },
                System = new StorageSystem() { Id = "888" },
                Uuid = 10203040,
                DestinationProcesses = new() { new ProcessDefinition() { Id = "999" } },
                Properties = new() { new PackageProperty() { Name = "AAA", Value = "BBB" } }
            };


            IPackage p3 = new Package()
            {
                BrutMass = 5,
                NetMass = 4,
                CreatedAt = DateTime.Parse("1/1/2024"),
                Id = "111",
                Category = new Category() { Id = "222" },
                Children = new() { new Package() { Id = "333" }, new Package() { Id = "444" } },
                Description = "555",
                Parent = new Package() { Id = "666" },
                Vessel = new Vessel() { Id = "787" },
                System = new StorageSystem() { Id = "888" },
                Uuid = 10203040,
                DestinationProcesses = new() { new ProcessDefinition() { Id = "999" } },
                Properties = new() { new PackageProperty() { Name = "AAA", Value = "BBB" } }
            };

            Assert.IsNotNull(p2);
            Assert.IsTrue(p2.Equals(p));

            p2.DestinationProcesses = new() { new ProcessDefinition() { Id = "989" } };
            Assert.IsFalse(p2.Equals(p));
            Assert.IsFalse(p3.Equals(p));
        }
    }
}