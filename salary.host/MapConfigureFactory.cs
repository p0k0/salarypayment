using AutoMapper;
using salary.domain;

namespace salary.host
{
    public class MapConfigureFactory : IMapConfigureFactory
    {
        private static IMapper _serviceMapper;
        private static IMapper _webApiMapper;
        
        public IMapper CreateServiceMap()
        {
            if (_serviceMapper == null)
            {
                _serviceMapper = new Mapper(new MapperConfiguration(_ =>
                {
                    _.CreateMap<salary.dto.Employee, OfficeEmployee>();
                    _.CreateMap<salary.dto.Employee, OutsourceEmployee>();
                    _.CreateMap<salary.dto.Employee, EmployeeBase>()
                        .Include<salary.dto.Employee, OfficeEmployee>()
                        .Include<salary.dto.Employee, OutsourceEmployee>()
                        .ConstructUsing((employee, context) =>
                        {
                            if (string.Equals(employee.Kind, EmployeeBase.KindHourly))
                            {
                                return new OutsourceEmployee();
                            }
                            return new OfficeEmployee();
                        });
                    _.CreateMap<EmployeeBase,salary.dto.Employee>();
                }));
            }
            
            return _serviceMapper;
        }
        
        public IMapper CreateWebApiMap()
        {
            if (_webApiMapper == null)
            {
                _webApiMapper = new Mapper(new MapperConfiguration(_ =>
                {
                    _.CreateMap<EmployeeBase, webapi.dto.Employee>();
                }));
            }
            return _webApiMapper;
        }
    }
}