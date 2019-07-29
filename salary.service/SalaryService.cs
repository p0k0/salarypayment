using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using salary.dal;
using salary.domain;
using salary.service.command;

namespace salary.service
{
    public sealed class SalaryService : ISalaryService
    {
        private readonly IRepository _repository;
        private IMapper _mapper;

        public SalaryService(IRepository repository)
        {
            _repository = repository;
            ConfigureMapper();
        }

        private void ConfigureMapper()
        {
            var config = new MapperConfiguration(_ =>
            {
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
            });
            _mapper = new Mapper(config);
        }
        
        public EmployeeBase Get(string name)
        {
            var cmd = new GetEmployeeCommand(name, _repository);
            cmd.Execute();
            return _mapper.Map<salary.dto.Employee, EmployeeBase>(cmd.Result);
        }

        public bool Save(EmployeeBase employee)
        {
            var cmd = new SaveEmployeeCommand(_mapper.Map<EmployeeBase, salary.dto.Employee>(employee), _repository);
            cmd.Execute();
            return cmd.IsSuccess;
        }

        public IEnumerable<EmployeeBase> GetMany(long limit, long offset)
        {
            var cmd = new GetManyEmployeeCommand(limit, offset, _repository);
            cmd.Execute();
            return cmd.Result.Select(_ => _mapper.Map<salary.dto.Employee, EmployeeBase>(_));
        }

        public EmployeeBase GetMostExpensiveEmployee(short kind)
        {
            throw new System.NotImplementedException();
        }

        public double GetMonthlyCost()
        {
            throw new System.NotImplementedException();
        }
    }
}