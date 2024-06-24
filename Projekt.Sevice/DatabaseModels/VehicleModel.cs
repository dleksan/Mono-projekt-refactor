using System.Numerics;

namespace Projekt.Sevice.DatabaseModels
{
    public class VehicleModel
    {
        public Guid Id { get; set; }
        public Guid MakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }


    }
}
