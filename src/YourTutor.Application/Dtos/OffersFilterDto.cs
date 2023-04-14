namespace YourTutor.Application.Dtos;

public sealed record OffersFilterDto(bool IsRemotely, bool IsRemotelyFiltered, int PriceFrom, int PriceTo);


