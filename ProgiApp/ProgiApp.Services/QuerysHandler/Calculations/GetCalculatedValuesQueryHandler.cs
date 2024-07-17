using MediatR;
using ProgiApp.Domain.Interfaces;
using ProgiApp.Domain.Models;
using ProgiApp.Services.Querys.Calculations;
using Type = ProgiApp.Domain.Models.Type;

namespace ProgiApp.Services.QuerysHandler.Calculations;

public class GetCalculatedValuesQueryHandler : IRequestHandler<GetCalculatedValuesQuery, IEnumerable<CalcualtedVehicle>>
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly ITypeRepository _typeRepositoryRepository;
    private readonly ISellerFeeRepository _sellerFeeRepository;
    private readonly IOtherFeeRepository _otherFeeRepository;
    private readonly IAssociationFeeRepository _associationFeeRepository;
    private readonly IBuyerFeeRepository _buyerFeeRepository;

    public GetCalculatedValuesQueryHandler(IVehicleRepository vehicleRepository,
        ITypeRepository typeRepository,
        ISellerFeeRepository sellerFeeRepository,
        IOtherFeeRepository otherFeeRepository,
        IAssociationFeeRepository associationFeeRepository,
        IBuyerFeeRepository buyerFeeRepository)
    {
        _vehicleRepository = vehicleRepository;
        _typeRepositoryRepository = typeRepository;
        _sellerFeeRepository = sellerFeeRepository;
        _associationFeeRepository = associationFeeRepository;
        _buyerFeeRepository = buyerFeeRepository;
        _otherFeeRepository = otherFeeRepository;

    }

    public async Task<IEnumerable<CalcualtedVehicle>> Handle(GetCalculatedValuesQuery request, CancellationToken cancellationToken)
    {
        var vehicles = await _vehicleRepository.GetAll();
        var types = await _typeRepositoryRepository.GetAll();
        var sellerFees = await _sellerFeeRepository.GetAll();
        var associationFees = await _associationFeeRepository.GetAll();
        var buyerFees = await _buyerFeeRepository.GetAll();
        var otherFees = await _otherFeeRepository.GetAll();

        var calculatedVehicle = CalculateSellerFee(vehicles.ToList(), sellerFees.ToList());
        calculatedVehicle = CalculateBuyerFee(calculatedVehicle, buyerFees.ToList());
        calculatedVehicle = CalculateType(calculatedVehicle, types.ToList());
        calculatedVehicle = CalculateAssociationFee(calculatedVehicle, associationFees.ToList());
        calculatedVehicle = CalculateOtherFee(calculatedVehicle, otherFees.ToList());
        calculatedVehicle = CalculateTotal(calculatedVehicle);

        return calculatedVehicle;
    }

    private List<CalcualtedVehicle> CalculateSellerFee(List<Vehicle> vehicles, List<SellerFee> sellerFees)
    {
        return vehicles
           .GroupJoin(
               sellerFees,
               vehicle => vehicle.TypeId,
               sellerFee => sellerFee.TypeId,
               (vehicle, sellerFeesGroup) => new CalcualtedVehicle(
                   vehicle.Id,
                   vehicleType: vehicle.TypeId.ToString(),
                   vehiclePrice: vehicle.VehiclePrice,
                   basicFee: 0,
                   specialFee: sellerFeesGroup.FirstOrDefault()?.FeeValue * vehicle.VehiclePrice,
                   associationFee: 0,
                   storageFee: 0,
                   total: 0)
               ).ToList();
    }

    private List<CalcualtedVehicle> CalculateBuyerFee(List<CalcualtedVehicle> vehicles, List<BuyerFee> buyerFees)
    {
        return vehicles
            .GroupJoin(
                buyerFees,
                vehicle => long.Parse(vehicle.vehicleType),
                buyerFee => buyerFee.TypeId,
                (vehicles, buyerFeesGroup) => new CalcualtedVehicle(
                    vehicles.id,
                    vehicles.vehicleType,
                    vehicles.vehiclePrice,
                    vehicles.vehiclePrice * buyerFeesGroup.FirstOrDefault()?.FeeValue <= buyerFeesGroup.FirstOrDefault()?.MinValue ? buyerFeesGroup.FirstOrDefault()?.MinValue :
                    vehicles.vehiclePrice * buyerFeesGroup.FirstOrDefault()?.FeeValue >= buyerFeesGroup.FirstOrDefault()?.MaxValue ? buyerFeesGroup.FirstOrDefault()?.MaxValue :
                    vehicles.vehiclePrice * buyerFeesGroup.FirstOrDefault()?.FeeValue,
                    vehicles.specialFee,
                    0,
                    0,
                    total: 0)
                ).ToList();
    }

    private List<CalcualtedVehicle> CalculateType(List<CalcualtedVehicle> vehicles, List<Type> types)
    {
        return vehicles
            .GroupJoin(
                types,
                vehicle => long.Parse(vehicle.vehicleType),
                types => types.Id,
                (vehicle, types) => new CalcualtedVehicle(
                    vehicle.id,
                    types.FirstOrDefault()?.VehicleType,
                    vehicle.vehiclePrice,
                    vehicle.basicFee,
                    vehicle.specialFee,
                    0,
                    0,
                    total: 0)
                ).ToList();
    }

    private List<CalcualtedVehicle> CalculateAssociationFee(List<CalcualtedVehicle> vehicles, List<AssociationFee> associationFees)
    {
        return vehicles.Select(vehicle =>
        {
            var fee = associationFees.FirstOrDefault(f =>
            vehicle.vehiclePrice >= f.MinValue &&
            vehicle.vehiclePrice <= f.MaxValue);

            return new CalcualtedVehicle(
                    vehicle.id,
                    vehicle.vehicleType,
                    vehicle.vehiclePrice,
                    vehicle.basicFee,
                    vehicle.specialFee,
                    fee?.FeeValue,
                    0,
                    total: 0);
        }).ToList();
    }

    private List<CalcualtedVehicle> CalculateOtherFee(List<CalcualtedVehicle> vehicles, List<OtherFee> otherFees)
    {
        return vehicles.Select(vehicle =>
            {
                return new CalcualtedVehicle(
                    vehicle.id,
                    vehicle.vehicleType,
                    vehicle.vehiclePrice,
                    vehicle.basicFee,
                    vehicle.specialFee,
                    vehicle.associationFee,
                    otherFees[0].FeeValue,
                    total: 0);
            }).ToList();
    }

    private List<CalcualtedVehicle> CalculateTotal(List<CalcualtedVehicle> vehicles)
    {
        return vehicles.Select(vehicle =>
        {
            var total = vehicle.vehiclePrice + vehicle.basicFee + vehicle.specialFee + vehicle.associationFee + vehicle.storageFee;
            return new CalcualtedVehicle(
                vehicle.id,
                vehicle.vehicleType,
                vehicle.vehiclePrice,
                vehicle.basicFee,
                vehicle.specialFee,
                vehicle.associationFee,
                vehicle.storageFee,
                total);
        }).ToList();
    }
}