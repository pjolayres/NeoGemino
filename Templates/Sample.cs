
using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace NeoGemino.Templates {
    public class Sample {

        public string Data { get; }

        public Sample(string data) {
            this.Data = data;
        }

        public void Render(string outputDirectory)
        {
            dynamic data = JObject.Parse(this.Data);
            var outputDirectoryPath = Path.Combine(outputDirectory, "Sample");

            if (Directory.Exists(outputDirectoryPath) == false) {
                Directory.CreateDirectory(outputDirectoryPath);
            }

            foreach (var entity in data.Entities)
            {
                foreach (var entityType in entity.Types) {
                    var output = $@"
                        using System;
                        using System.Collections;

                        namespace {entity.Namespace}
                        {{
                            public partial class {entityType.Name}
                            {{
                                #region [   Public Properties   ]

                                {
                                    string.Join($@"

                                ", ((JArray)entityType.Properties).Select(entityTypeProperty => $@"
                                public {entityTypeProperty["Type"]} {entityTypeProperty["Name"]} {{ get; set; }}"
                                    .TrimStart()).ToArray())
                                }
                                {
                                    this.RenderRelationships(data, entity, entityType)
                                }
                                #endregion

                                public {entityType.Name}()
                                {{
                                    //TODO: Initialize children
                                }}
                            }}
                        }}";

                    //var cleanedOutput = output;
                    var cleanedOutput = Utilities.TrimLineStart(output).Trim();

                    var filePath = Path.Combine(outputDirectoryPath, $"{entityType.Name}.cs");
                    File.WriteAllText(filePath, cleanedOutput);

                    Console.WriteLine($"Created file '{filePath}'");
                }
            }
        }

        private string RenderRelationships(JObject data, JObject entity, JObject entityType)
        {
            var entityTypes = data["Entities"].SelectMany(x => x["Types"]).ToArray();

            var relationships = ((JArray)data["Relationships"]).Where(x => (string)x["ChildEntityType"] == (string)entityType["FullName"]).ToArray();
            
            var properties = from relationship in relationships
                             select new {
                                 Type = entityTypes.Where(x => (string)x["FullName"] == (string)relationship["ParentEntityType"]).Select(x => x["Name"]).FirstOrDefault() ?? relationship["ParentEntityType"],
                                 Name = relationship["ParentReferenceProperty"]
                             };

            var result = string.Join($@"

                                ", properties.Select(x => $@"
                                public {x.Type} {x.Name} {{ get; set; }}".TrimStart()).ToArray());

            if (string.IsNullOrWhiteSpace(result) == false)
            {
                result = $@"
                                {result}
                                ";
            }
            
            return result;
        }
    }
}