using MediatR;
using StoreManager.Application.Common;
using StoreManager.Application.Product.Batch.DTO;

namespace StoreManager.Application.Product.Batch.Command;

public record CreateProductBatchCommand(string ProductId, int Quantity) : IRequest<Result<CreateProductBatchResponseDto>>;