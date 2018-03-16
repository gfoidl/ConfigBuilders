using System.ServiceModel;

namespace gfoidl.ConfigBuilders.Tests.WCF
{
    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        string SayHi();
    }
}
