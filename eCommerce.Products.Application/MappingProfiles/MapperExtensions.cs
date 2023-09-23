using AutoMapper;
using eCommerce.Products.Application.Abstractions;

namespace eCommerce.Products.Application.MappingProfiles;

public static class MapperExtensions
{
    public static Result<U> MapToResult<T, U>(this IMapper mapper, T value) =>
        Result.Ok(mapper.Map<U>(value));
}
