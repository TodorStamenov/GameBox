using MediatR;
using Newtonsoft.Json.Linq;

namespace GameBox.Application.GraphQL.Queries.GraphQlQuery
{
    public class GraphQlQuery : IRequest<object>
    {
        public string OperationName { get; set; }

        public string Query { get; set; }

        public JObject Variables { get; set; }
    }
}
