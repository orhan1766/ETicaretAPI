﻿
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Customers.ValidationRules;

namespace Business.Handlers.Customers.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateCustomerCommand : IRequest<IResult>
    {

        public System.DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public System.Collections.Generic.ICollection<Order> Orders { get; set; }


        public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, IResult>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IMediator _mediator;
            public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IMediator mediator)
            {
                _customerRepository = customerRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateCustomerValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                var isThereCustomerRecord = _customerRepository.Query().Any(u => u.CreatedDate == request.CreatedDate);

                if (isThereCustomerRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedCustomer = new Customer
                {
                    CreatedDate = request.CreatedDate,
                    Name = request.Name,
                    Orders = request.Orders,

                };

                _customerRepository.Add(addedCustomer);
                await _customerRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}