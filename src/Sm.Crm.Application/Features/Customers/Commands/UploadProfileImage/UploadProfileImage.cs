using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Sm.Crm.Domain.Repositories;

namespace Sm.Crm.Application.Features.Customers.Commands.UploadProfileImage;

public class UploadProfileImageCommand : IRequest<string>
{
    public long CustomerId { get; set; }
    public string? ProfileImageUrl { get; set; }
    public IFormFile ProfileImage { get; set; }
}

public class UploadProfileImageCommandHandler : IRequestHandler<UploadProfileImageCommand, string>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public UploadProfileImageCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<string> Handle(UploadProfileImageCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetById(request.CustomerId);
        if (customer != null)
        {
            customer.ProfileImageUrl = request.ProfileImageUrl;
            await _customerRepository.Update(customer);
            return request.ProfileImageUrl;
        }
        return "";
    }
}