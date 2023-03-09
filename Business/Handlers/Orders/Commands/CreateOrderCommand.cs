
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
using Business.Handlers.Orders.ValidationRules;

namespace Business.Handlers.Orders.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOrderCommand : IRequest<IResult>
    {

        public int CustomerId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public System.Collections.Generic.ICollection<Product> Products { get; set; }


        public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, IResult>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IMediator _mediator;
            public CreateOrderCommandHandler(IOrderRepository orderRepository, IMediator mediator)
            {
                _orderRepository = orderRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOrderValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
            {
                var isThereOrderRecord = _orderRepository.Query().Any(u => u.CustomerId == request.CustomerId);

                if (isThereOrderRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOrder = new Order
                {
                    CustomerId = request.CustomerId,
                    CreatedDate = request.CreatedDate,
                    Address = request.Address,
                    Description = request.Description,
                    Products = request.Products,

                };

                _orderRepository.Add(addedOrder);
                await _orderRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}