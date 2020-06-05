using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerSolutionsApp.Web.HostedServices
{
    internal interface IDataProcessingService
    {
        Task DoWork(CancellationToken cancellationToken);
    }
}
