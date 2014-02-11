using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;
//Jack
namespace BrentwoodPrinting.Data.FormBuilder
{
    public static class FormBuilder
    {
        public static HtmlTable CreateTable(int jobID)
        {
            HtmlTable table = new HtmlTable();
            List<JobControl_Get_Result> results = Brentwood.ListJobControlByJobType(jobID);

            foreach (JobControl_Get_Result r in results)
                table.Rows.Add(AddRow(r));

            table = AddStandardControls(table);

            return table;
        }

        private static HtmlTableRow AddRow(JobControl_Get_Result controlResult)
        {
            HtmlTableRow row = new HtmlTableRow();
            row.Cells.Add(AddLabel(controlResult.ControlName));
            row.Cells.Add(AddControl(controlResult));
            return row;
        }

        private static HtmlTableCell AddLabel(string labelText)
        {
            HtmlTableCell cell = new HtmlTableCell();
            Label l = new Label();
            l.Text = labelText;
            cell.Controls.Add(l);
            return cell;
        }

        private static HtmlTableCell AddControl(JobControl_Get_Result control)
        {
            HtmlTableCell cell = new HtmlTableCell();
            Control c = new Control();

            switch (control.ControlTypeName)
            {
                case "TextBox":
                    c = new TextBox();
                    c.ID = control.JobControlID + control.ControlTypeName + control.ControlName;
                    break;

                case "CheckBox":
                    c = new CheckBox();
                    c.ID = control.JobControlID + control.ControlTypeName + control.ControlName;
                    break;

                case "ImageUpload":
                    c = new FileUpload();
                    c.ID = control.JobControlID + control.ControlTypeName + control.ControlName;
                    break;
            }

            cell.Controls.Add(c);
            return cell;
        }

        private static HtmlTable AddStandardControls(HtmlTable table)
        {
            HtmlTableRow row = new HtmlTableRow();
            row.Cells.Add(AddLabel("Quantity"));
            row.Cells.Add(AddControl(JobControl_Get_Result.CreateJobControl_Get_Result(500, "QuantityTextbox", "TextBox")));
            table.Rows.Add(row);

            row = new HtmlTableRow();
            row.Cells.Add(AddLabel("Special Instructions"));
            TextBox box = new TextBox();
            box.ID = "501TextBoxSpecialInstructionsTextbox";
            box.TextMode = TextBoxMode.MultiLine;
            HtmlTableCell cell = new HtmlTableCell();
            cell.Controls.Add(box);
            row.Cells.Add(cell);
            table.Rows.Add(row);

            row = new HtmlTableRow();
            row.Cells.Add(AddLabel("Delivery?"));
            row.Cells.Add(AddControl(JobControl_Get_Result.CreateJobControl_Get_Result(502, "DeliveryCheckbox", "CheckBox")));
            table.Rows.Add(row);

            return table;
        }
    }
}
