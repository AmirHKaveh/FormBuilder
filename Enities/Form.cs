using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormBuilderDemo.Enities
{
    public class Form
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="عنوان")]
        [Required(ErrorMessage ="لطفا {0} را وارد نمایید")]
        [MaxLength(200)]
        public string Title { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100)]
        public string Name { get; set; }

        public IList<FormField> FormFields { get; set; }
        public IList<FormData> FormDatas { get; set; }
    }
}
