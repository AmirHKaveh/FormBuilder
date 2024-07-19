using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FormBuilderDemo.Enities
{
    public enum FieldTypes
    {
        [Display(Name = "ورودی متن")]
        Text,
        [Display(Name = "ورودی عدد")]
        Number,
        [Display(Name = "چک باکس")]
        Checkbox,
        [Display(Name = "دکمه رادیو")]
        Radio,
        [Display(Name = "چندانتخابی")]
        Selectbox,
        [Display(Name = "فایل آپلود")]
        File

    }
    public class FormField
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200)]
        public string Title { get; set; }
        [Display(Name = "شناسه فیلد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100)]
        public string UniqueName { get; set; }
        [ForeignKey("Form")]
        public int FormId { get; set; }
        [Display(Name = "نوع فیلد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public FieldTypes FieldType { get; set; }
        [Display(Name = "اجباری")]
        public bool IsRequired { get; set; }
        [Display(Name = "اندازه بیشینه")]
        [MaxLength(4)]
        public string MaxLength { get; set; }
        [Display(Name = "آیتم ها")]
        [MaxLength(100)]
        public string Options { get; set; }

        public Form Form { get; set; }
    }
}
