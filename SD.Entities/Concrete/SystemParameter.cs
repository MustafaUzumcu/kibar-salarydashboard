
namespace SD.Entities.Concrete
{
    /// <summary>
    /// Salary Dashboard Kontrol Parametreleri
    /// </summary>
    public class SystemParameter:IEntity
    {
        public int ParameterId { get; set; }

        public string ParameterName { get; set; }

        public string ParameterValue { get; set; }

        public string Description { get; set; }

        public bool IsReadOnly { get; set; }
    }
}
