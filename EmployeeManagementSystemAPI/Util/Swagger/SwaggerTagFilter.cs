using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EmployeeManagementSystemAPI.Util.Swagger
{
    public class SwaggerTagFilter<T> : IDocumentFilter where T : ISwaggerTags, new()
    {
        private static readonly List<string> TagNames = new T().TagNames;

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (swaggerDoc.Tags == null)
            {
                IList<OpenApiTag> list2 = (swaggerDoc.Tags = new List<OpenApiTag>());
            }

            AddTags(swaggerDoc.Tags);
            swaggerDoc.Tags = swaggerDoc.Tags.OrderBy((OpenApiTag x) => x.Name).ToList();
        }

        private static void AddTags(ICollection<OpenApiTag> tags)
        {
            foreach (string tagName in TagNames)
            {
                tags.Add(new OpenApiTag
                {
                    Name = tagName
                });
            }
        }
    }
}