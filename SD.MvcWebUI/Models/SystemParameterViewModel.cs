using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SD.Entities.Concrete;

namespace SD.MvcWebUI.Models
{
    public class SystemParameterViewModel
    {
        public int ParameterId { get; set; }

        [Required(ErrorMessage = "Parametre Adı alanı zorunlu")]
        public string ParameterName { get; set; }

        [Required(ErrorMessage = "Parametre Değeri alanı zorunlu")]
        public string ParameterValue { get; set; }

        [Required(ErrorMessage = "Açıklama alanı zorunlu")]
        public string Description { get; set; }

        public bool IsReadOnly { get; set; }
    }

    public class SystemParameterListModel
    {
        public IList<SystemParameter> SystemParameters { get; set; }
    }

}
