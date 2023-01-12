using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Tools.Models;
using Tools.Tools.Grid;

namespace Tools.Service
{

    public class ResponseResult
    {
        public bool isSuccessful { get; set; }
        public List<Column> cols { get; set; }
        public string error { get; set; }
        public object pk { get; set; }

        public ResponseResult( List<Column> cols, bool isSuccessful = true)
        {
            this.isSuccessful = isSuccessful;
            this.cols = cols;
            this.pk = this.cols.FirstOrDefault(w => w.FieldType == fieldType.PK).CellValue;
        }

        public ResponseResult(string err, bool isSuccessful = false)
        {
            this.isSuccessful = isSuccessful;
            this.error = err;
        }


    }
}
