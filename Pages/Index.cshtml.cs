using FormBuilderDemo.Context;
using FormBuilderDemo.DTO;
using FormBuilderDemo.Enities;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FormBuilderDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly string _filePath = "files";
        private readonly ILogger<IndexModel> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _db;
        public IndexModel(ILogger<IndexModel> logger, AppDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> OnGet()
        {
            var forms = await _db.Form_tb.ToListAsync();
            var formItems = new SelectList(forms, "Id", "Title");
            TempData["Forms"] = formItems;
            return Page();
        }
        public IActionResult OnGetCreateForm()
        {
            return Partial("_CreateForm");
        }
        public async Task<IActionResult> OnPostCreateForm(Form form)
        {
            if (ModelState.IsValid)
            {
                if (await _db.Form_tb.AnyAsync(x => x.Name == form.Name))
                    return new JsonResult(new { status = "exist" });

                await _db.Form_tb.AddAsync(form);
                await _db.SaveChangesAsync();
                return new JsonResult(new { status = "ok", id = form.Id });
            }
            return Page();
        }
        public async Task<IActionResult> OnPostCreateFormFields(FormField formField)
        {
            if (ModelState.IsValid)
            {
                if (await _db.FormField_tb.AnyAsync(x => x.UniqueName == formField.UniqueName))
                    return new JsonResult(new { status = "exist" });

                await _db.FormField_tb.AddAsync(formField);
                await _db.SaveChangesAsync();
                return new JsonResult(new { status = "ok" });
            }
            return Page();
        }

        public async Task<IActionResult> OnGetViewAllFieldsOfForm(int formId)
        {
            var fields = await _db.FormField_tb
                .Where(x => x.FormId == formId)
                .Include(s => s.Form)
                .ToListAsync();

            return Partial("_ViewFields", fields);
        }
        public async Task<IActionResult> OnGetEditField(int id)
        {
            var field = await _db.FormField_tb
                .Include(x => x.Form)
                .FirstOrDefaultAsync(x => x.Id == id);



            return Partial("_EditField", field);
        }

        public async Task<IActionResult> OnPostEditForm(Form form)
        {
            if (ModelState.IsValid)
            {
                if (await _db.Form_tb.AnyAsync(x => x.Id != form.Id && x.Name == form.Name))
                    return new JsonResult(new { status = "exist" });

                _db.Form_tb.Update(form);
                await _db.SaveChangesAsync();
                return new JsonResult(new { status = "ok" });
            }
            return Page();
        }

        public async Task<IActionResult> OnPostEditFormFields(FormField formField)
        {
            if (ModelState.IsValid)
            {
                if (await _db.FormField_tb.AnyAsync(x => x.Id != formField.Id && x.UniqueName == formField.UniqueName))
                    return new JsonResult(new { status = "exist" });

                if (formField.FieldType != FieldTypes.Selectbox)
                    formField.Options = null;

                _db.FormField_tb.Update(formField);
                await _db.SaveChangesAsync();
                return new JsonResult(new { status = "ok" });
            }
            return Page();
        }

        public async Task<IActionResult> OnPostSubmitForm(FormDataDTO data)
        {
            var frmId = data.FormId;
            var obj = JObject.Parse(data.FormValues);
            obj.Property("__RequestVerificationToken").Remove();
            var path = String.Format("{0}/{1}", _webHostEnvironment.WebRootPath, _filePath);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);


            #region FilesName
            if (!string.IsNullOrEmpty(data.FilesName))
            {
                var namesString = data.FilesName.Remove(data.FilesName.LastIndexOf('='));
                var namesList = namesString.Split('=');
                Dictionary<string, string> namesDic = new Dictionary<string, string>();
                foreach (var row in namesList)
                {
                    var item = row.Split('|');
                    namesDic.Add(item[0], item[1]);
                }

                #endregion
                foreach (var item in data.Files)
                {
                    var currentfileName = item.FileName;
                    var names = namesDic.Where(x => x.Value == currentfileName);
                    if (names.Any())
                    {
                        var name = names.FirstOrDefault();
                        var gotName = name.Key; //Title of field

                        var filePath = String.Format("/{0}/{1}", _filePath, item.FileName);
                        var fullFilePath = String.Format("{0}/{1}", path, item.FileName);
                        using (var stream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            await item.CopyToAsync(stream);
                        }

                        obj.Add(gotName, filePath);
                    }

                }
            }
            _db.FormData_tb.Add(new FormData()
            {
                FormValue = JsonConvert.SerializeObject(obj),
                FormId = frmId
            });
            await _db.SaveChangesAsync();
            return new JsonResult("ok");
        }

        public async Task<IActionResult> OnGetFormFieldsTitle(int formId)
        {
            var formFields = await _db.FormField_tb.Where(x => x.FormId == formId).ToListAsync();

            var fieldsValues = await _db.FormData_tb.Where(x => x.FormId == formId).ToListAsync();

            TempData["FieldsValues"] = fieldsValues;
            return Partial("_FormFields", formFields);
        }

    }
}
