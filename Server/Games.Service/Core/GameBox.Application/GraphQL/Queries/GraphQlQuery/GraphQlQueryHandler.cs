using GraphQL;
using GraphQL.Types;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GameBox.Application.GraphQL.Queries.GraphQlQuery
{
    public class GraphQlQueryHandler : IRequestHandler<GraphQlQuery, object>
    {
        private readonly ISchema schema;
        private readonly IDocumentExecuter executer;

        public GraphQlQueryHandler(ISchema schema, IDocumentExecuter executer)
        {
            this.schema = schema;
            this.executer = executer;
        }

        public async Task<object> Handle(GraphQlQuery request, CancellationToken cancellationToken)
        {
            return await executer.ExecuteAsync(o =>
            {
                o.Schema = schema;
                o.Query = request.Query;
                o.OperationName = request.OperationName;
                o.Inputs = request.Variables?.ToInputs();
            });
        }
    }
}