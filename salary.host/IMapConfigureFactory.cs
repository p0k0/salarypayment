using AutoMapper;

namespace salary.host
{
    public interface IMapConfigureFactory
    {
        IMapper CreateServiceMap();
        IMapper CreateWebApiMap();
    }
}