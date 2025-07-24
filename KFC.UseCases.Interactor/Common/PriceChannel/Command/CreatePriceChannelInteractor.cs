using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.Entities.Utility;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;
using KFC.Entities.Enums;

namespace KFC.UseCases.Interactor;

public class CreatePriceChannelInteractor : ICreatePriceChannelInputPort
{
    private readonly ICreatePriceChannelOutputPort _outputPort;
    private readonly IPriceChannelRepository _repository;
    private readonly IChannelRepository _channelRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInputPortValidator<CreatePriceChannelDto> _validator;

    public CreatePriceChannelInteractor(
        IUnitOfWork unitOfWork,
        IPriceChannelRepository repository,
        IChannelRepository channelRepository,
        IProductRepository productRepository,
        ICreatePriceChannelOutputPort outputPort,
        IInputPortValidator<CreatePriceChannelDto> validator 
        )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _channelRepository = channelRepository;
        _productRepository = productRepository;
        _outputPort = outputPort;
        _validator = validator;
    }

    public async Task Handle(CreatePriceChannelDto inputDto)
    {
        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }

        var product = await _productRepository.GetProductByCodeAsync(inputDto.ProductId);
        var channel = await _channelRepository.GetChannelByCodeAsync(inputDto.ChannelId);   

        PriceChannel PriceChannel = new PriceChannel(
           productId: product!.ProductId,
           channelId: channel!.ChannelId,
           amount: inputDto.Amount,
           isActive: true);

        await _repository.CreateAsync(PriceChannel);
        await _unitOfWork.SaveChanges();
        await _outputPort.HandleSuccess(HandleSuccess<string>.Created(data: PriceChannel.PriceChannelId.ToString()));

    }

}

