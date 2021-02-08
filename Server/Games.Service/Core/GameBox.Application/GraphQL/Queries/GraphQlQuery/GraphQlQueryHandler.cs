using GameBox.Application.Contracts.Services;
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
        private readonly ICurrentUserService currentUser;

        public GraphQlQueryHandler(
            ISchema schema,
            IDocumentExecuter executer,
            ICurrentUserService currentUser)
        {
            this.schema = schema;
            this.executer = executer;
            this.currentUser = currentUser;
        }

        public async Task<object> Handle(GraphQlQuery request, CancellationToken cancellationToken)
        {
            request.Variables.Add("userId", this.currentUser.UserId);
            request.Variables.Add("customerId", this.currentUser.CustomerId);

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