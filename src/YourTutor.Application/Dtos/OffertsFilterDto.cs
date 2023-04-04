namespace YourTutor.Application.Dtos;

public sealed record OffertsFilterDto(bool IsRemotely, bool IsRemotelyFiltered, int PriceFrom, int PriceTo);


