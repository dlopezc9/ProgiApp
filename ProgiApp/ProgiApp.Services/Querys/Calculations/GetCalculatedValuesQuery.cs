using MediatR;

namespace ProgiApp.Services.Querys.Calculations;

public record GetCalculatedValuesQuery() : IRequest<IEnumerable<CalcualtedVehicle>>;

public record CalcualtedVehicle(
    int id,
    string vehicleType,
    decimal? vehiclePrice,
    decimal? basicFee,
    decimal? specialFee,
    int? associationFee,
    int? storageFee,
    decimal? total);