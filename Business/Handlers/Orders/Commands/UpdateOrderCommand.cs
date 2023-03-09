
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Orders.ValidationRules;


namespace Business.Handlers.Orders.Commands
{


    public class UpdateOrderCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public System.Collections.Generic.ICollection<Product> Products { get; set; }

        public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, IResult>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly IMediator _mediator;

            public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMediator mediator)
            {
                _orderRepository = orderRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOrderValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
            {
                var isThereOrderRecord = await _orderRepository.GetAsync(u => u.Id == request.Id);


                isThereOrderRecord.CustomerId = request.CustomerId;
                isThereOrderRecord.CreatedDate = request.CreatedDate;
                isThereOrderRecord.Address = request.Address;
                isThereOrderRecord.Description = request.Description;
                isThereOrderRecord.Products = request.Products;


                _orderRepository.Update(isThereOrderRecord);
                await _orderRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

