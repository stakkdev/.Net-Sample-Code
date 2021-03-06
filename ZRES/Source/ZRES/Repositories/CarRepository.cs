namespace ZRES.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ZRES.Models;

    public class CarRepository : ICarRepository
    {
        private static readonly List<Car> Cars = new List<Car>()
        {
            new Car()
            {
                CarId = 1,
                Created = DateTimeOffset.UtcNow.AddDays(-8),
                Cylinders = 8,
                Make = "Lambourghini",
                Model = "Countach",
                Modified = DateTimeOffset.UtcNow.AddDays(-8),
            },
            new Car()
            {
                CarId = 2,
                Created = DateTimeOffset.UtcNow.AddDays(-7),
                Cylinders = 10,
                Make = "Mazda",
                Model = "Furai",
                Modified = DateTimeOffset.UtcNow.AddDays(-6),
            },
            new Car()
            {
                CarId = 3,
                Created = DateTimeOffset.UtcNow.AddDays(-7),
                Cylinders = 6,
                Make = "Honda",
                Model = "NSX",
                Modified = DateTimeOffset.UtcNow.AddDays(-3),
            },
            new Car()
            {
                CarId = 4,
                Created = DateTimeOffset.UtcNow.AddDays(-5),
                Cylinders = 6,
                Make = "Lotus",
                Model = "Esprit",
                Modified = DateTimeOffset.UtcNow.AddDays(-3),
            },
            new Car()
            {
                CarId = 5,
                Created = DateTimeOffset.UtcNow.AddDays(-4),
                Cylinders = 6,
                Make = "Mitsubishi",
                Model = "Evo",
                Modified = DateTimeOffset.UtcNow.AddDays(-2),
            },
            new Car()
            {
                CarId = 6,
                Created = DateTimeOffset.UtcNow.AddDays(-4),
                Cylinders = 12,
                Make = "McLaren",
                Model = "F1",
                Modified = DateTimeOffset.UtcNow.AddDays(-1),
            },
        };

        public Task<Car> AddAsync(Car car, CancellationToken cancellationToken)
        {
            if (car is null)
            {
                throw new ArgumentNullException(nameof(car));
            }

            Cars.Add(car);
            car.CarId = Cars.Max(x => x.CarId) + 1;
            return Task.FromResult(car);
        }

        public Task DeleteAsync(Car car, CancellationToken cancellationToken)
        {
            if (Cars.Contains(car))
            {
                Cars.Remove(car);
            }

            return Task.CompletedTask;
        }

        public Task<Car> GetAsync(int carId, CancellationToken cancellationToken)
        {
            var car = Cars.FirstOrDefault(x => x.CarId == carId);
            return Task.FromResult(car);
        }

        public Task<List<Car>> GetCarsAsync(
            int? first,
            DateTimeOffset? createdAfter,
            DateTimeOffset? createdBefore,
            CancellationToken cancellationToken) =>
            Task.FromResult(Cars
                .If(createdAfter.HasValue, x => x.Where(y => y.Created > createdAfter.Value))
                .If(createdBefore.HasValue, x => x.Where(y => y.Created < createdBefore.Value))
                .If(first.HasValue, x => x.Take(first.Value))
                .ToList());

        public Task<List<Car>> GetCarsReverseAsync(
            int? last,
            DateTimeOffset? createdAfter,
            DateTimeOffset? createdBefore,
            CancellationToken cancellationToken) =>
            Task.FromResult(Cars
                .If(createdAfter.HasValue, x => x.Where(y => y.Created > createdAfter.Value))
                .If(createdBefore.HasValue, x => x.Where(y => y.Created < createdBefore.Value))
                .If(last.HasValue, x => x.TakeLast(last.Value))
                .ToList());

        public Task<bool> GetHasNextPageAsync(
            int? first,
            DateTimeOffset? createdAfter,
            CancellationToken cancellationToken) =>
            Task.FromResult(Cars
                .If(createdAfter.HasValue, x => x.Where(y => y.Created > createdAfter.Value))
                .Skip(first.Value)
                .Any());

        public Task<bool> GetHasPreviousPageAsync(
            int? last,
            DateTimeOffset? createdBefore,
            CancellationToken cancellationToken) =>
            Task.FromResult(Cars
                .If(createdBefore.HasValue, x => x.Where(y => y.Created < createdBefore.Value))
                .SkipLast(last.Value)
                .Any());

        public Task<int> GetTotalCountAsync(CancellationToken cancellationToken) => Task.FromResult(Cars.Count);

        public Task<Car> UpdateAsync(Car car, CancellationToken cancellationToken)
        {
            if (car is null)
            {
                throw new ArgumentNullException(nameof(car));
            }

            var existingCar = Cars.FirstOrDefault(x => x.CarId == car.CarId);
            existingCar.Cylinders = car.Cylinders;
            existingCar.Make = car.Make;
            existingCar.Model = car.Model;
            return Task.FromResult(car);
        }
    }
}
