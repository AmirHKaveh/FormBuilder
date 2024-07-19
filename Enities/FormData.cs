using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FormBuilderDemo.Enities
{
    public class FormData
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "اطلاعات فرم")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string FormValue { get; set; }
        [ForeignKey("Form")]
        public int FormId { get; set; }
        public Form Form { get; set; }
    }
}
