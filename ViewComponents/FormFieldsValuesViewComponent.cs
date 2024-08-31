using FormBuilderDemo.Context;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormBuilderDemo.ViewComponents
{
    public class FormFieldsValuesViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;

        public FormFieldsValuesViewComponent(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(int formId, string uniqueName)
        {
            string value = "";
            var fieldsValues = await _db.FormData_tb.FirstOrDefaultAsync(x => x.FormId == formId);
            if (fieldsValues is not null)
            {
                var formValue = JObject.Parse(fieldsValues.FormValue);
                value = formValue[uniqueName].ToString();
            }

            return await Task.FromResult((IViewComponentResult)View("FormFieldsValues", value));
        }
    }
}
