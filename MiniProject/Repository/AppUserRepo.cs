using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniProject.DTOs;
using MiniProject.JWT;
using MiniProject.Models;
using MiniProject.Models.DBContext;
using MiniProject.Repository.Interfaces;
using Syncfusion.XlsIO;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.XlsIO;
using System.IO;
using Syncfusion.Drawing;
using Syncfusion.XlsIO.Calculate;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using System.Collections.Generic;
using Color = Syncfusion.Drawing.Color;

namespace MiniProject.Repository
{
    public class AppUserRepo : IAppUserRepo
    {
        private readonly MiniProjectDBContext _context;
        private readonly IMapper _mapper;
        private readonly JWTService _jWTService;
        private readonly SendGridEmailSender _sendGridEmailSender;

        public AppUserRepo(MiniProjectDBContext miniProjectDBContext, IMapper mapper, JWTService jWTService, SendGridEmailSender sendGridEmailSender)
        {
            _context = miniProjectDBContext;
            _mapper = mapper;
            _jWTService = jWTService;
            _sendGridEmailSender = sendGridEmailSender;
        }

        public UserDTO Decode(string token)
        {
            var x = _jWTService.DecodeToken(token);
            UserDTO userDTO = new UserDTO();
            userDTO.RoleId = x.RoleId;
            userDTO.EmpId = x.EmpId;
            return userDTO;
        }

        public async Task<string> Login(LoginDTO loginDTO)
        {
            var check = await _context.AppUser.FirstOrDefaultAsync
                (o => o.EmployeeId == loginDTO.EmpId && o.Password == loginDTO.Password);
            if(check == null)
            {
                throw new ServiceException("No User Exists");
            }
            UserDTO userDTO = new UserDTO();
            userDTO.EmpId = check.EmployeeId;
            userDTO.RoleId = check.Role;
            var token = _jWTService.GenerateJSONWebToken(userDTO);
            var res = _mapper.Map<AppUserDTO>(check);
            return token;
            
        }

        public async Task<AppUserDTO> Register(RegistrationDTO registrationDTO)
        {
            var check = await _context.Employees.FirstOrDefaultAsync(o => o.Id == registrationDTO.EmpId);
            if (check == null)
            {
                throw new ServiceException("No Employee Exists");
            }
            var userCheck = await _context.AppUser.FirstOrDefaultAsync(o => o.EmployeeId == registrationDTO.EmpId);
            if(userCheck != null)
            {
                throw new ServiceException("User Already Exists");
            }
            AppUserDTO appUserDTO = new AppUserDTO();
            appUserDTO.EmployeeId = registrationDTO.EmpId;
            appUserDTO.Password = registrationDTO.Password;
            appUserDTO.Role = 2;
            var res = _mapper.Map<AppUser>(appUserDTO);
            res.CreatedDate = DateTime.Now;
            await _context.AddAsync(res);
            await _context.SaveChangesAsync();
            SendEmailDTO sendEmailDTO = new SendEmailDTO();
            sendEmailDTO.email = check.Email;
            sendEmailDTO.subject = "Welcome " + check.Name;
            sendEmailDTO.message = check.Name + ", You have Successfully Registered ";


            await _sendGridEmailSender.SendEmailAsync(sendEmailDTO);
            return appUserDTO;
        }

        public FileStreamResult export()
        {
            //Create an instance of ExcelEngine
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;

                application.DefaultVersion = ExcelVersion.Xlsx;

                //Create a workbook
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                //Adding a picture

                //FileStream imageStream = new FileStream("AdventureCycles-Logo.png", FileMode.Open, FileAccess.Read);
                //IPictureShape shape = worksheet.Pictures.AddPicture(1, 1, imageStream, 20, 20);

                //Disable gridlines in the worksheet
                //worksheet.IsGridLinesVisible = false;

                //Enter values to the cells from A3 to A5
                worksheet.Range["A3"].Text = "46036 Michigan Ave";
                worksheet.Range["A4"].Text = "Canton, USA";
                worksheet.Range["A5"].Text = "Phone: +1 231-231-2310";

                //Make the text bold
                worksheet.Range["A3:A5"].CellStyle.Font.Bold = true;

                //Merge cells
                worksheet.Range["D1:E1"].Merge();

                //Enter text to the cell D1 and apply formatting.
                worksheet.Range["D1"].Text = "INVOICE";
                worksheet.Range["D1"].CellStyle.Font.Bold = true;
                worksheet.Range["D1"].CellStyle.Font.RGBColor = Color.FromArgb(42, 118, 189);
                worksheet.Range["D1"].CellStyle.Font.Size = 35;

                //Apply alignment in the cell D1
                worksheet.Range["D1"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
                worksheet.Range["D1"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignTop;

                //Enter values to the cells from D5 to E8
                worksheet.Range["D5"].Text = "INVOICE#";
                worksheet.Range["E5"].Text = "DATE";
                worksheet.Range["D6"].Number = 1028;
                worksheet.Range["E6"].Value = "12/31/2018";
                worksheet.Range["D7"].Text = "CUSTOMER ID";
                worksheet.Range["E7"].Text = "TERMS";
                worksheet.Range["D8"].Number = 564;
                worksheet.Range["E8"].Text = "Due Upon Receipt";

                //Apply RGB backcolor to the cells from D5 to E8
                worksheet.Range["D5:E5"].CellStyle.Color = Color.FromArgb(42, 118, 189);
                worksheet.Range["D7:E7"].CellStyle.Color = Color.FromArgb(42, 118, 189);

                //Apply known colors to the text in cells D5 to E8
                worksheet.Range["D5:E5"].CellStyle.Font.Color = ExcelKnownColors.White;
                worksheet.Range["D7:E7"].CellStyle.Font.Color = ExcelKnownColors.White;

                //Make the text as bold from D5 to E8
                worksheet.Range["D5:E8"].CellStyle.Font.Bold = true;

                //Apply alignment to the cells from D5 to E8
                worksheet.Range["D5:E8"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                worksheet.Range["D5:E5"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
                worksheet.Range["D7:E7"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
                worksheet.Range["D6:E6"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignTop;

                //Enter value and applying formatting in the cell A7
                worksheet.Range["A7"].Text = "  BILL TO";
                worksheet.Range["A7"].CellStyle.Color = Color.FromArgb(42, 118, 189);
                worksheet.Range["A7"].CellStyle.Font.Bold = true;
                worksheet.Range["A7"].CellStyle.Font.Color = ExcelKnownColors.White;

                //Apply alignment
                worksheet.Range["A7"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
                worksheet.Range["A7"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

                //Enter values in the cells A8 to A12
                worksheet.Range["A8"].Text = "Steyn";
                worksheet.Range["A9"].Text = "Great Lakes Food Market";
                worksheet.Range["A10"].Text = "20 Whitehall Rd";
                worksheet.Range["A11"].Text = "North Muskegon,USA";
                worksheet.Range["A12"].Text = "+1 231-654-0000";

                //Create a Hyperlink for e-mail in the cell A13
                IHyperLink hyperlink = worksheet.HyperLinks.Add(worksheet.Range["A13"]);
                hyperlink.Type = ExcelHyperLinkType.Url;
                hyperlink.Address = "Steyn@greatlakes.com";
                hyperlink.ScreenTip = "Send Mail";

                //Merge column A and B from row 15 to 22
                worksheet.Range["A15:B15"].Merge();
                worksheet.Range["A16:B16"].Merge();
                worksheet.Range["A17:B17"].Merge();
                worksheet.Range["A18:B18"].Merge();
                worksheet.Range["A19:B19"].Merge();
                worksheet.Range["A20:B20"].Merge();
                worksheet.Range["A21:B21"].Merge();
                worksheet.Range["A22:B22"].Merge();

                //Enter details of products and prices
                worksheet.Range["A15"].Text = "  DESCRIPTION";
                worksheet.Range["C15"].Text = "QTY";
                worksheet.Range["D15"].Text = "UNIT PRICE";
                worksheet.Range["E15"].Text = "AMOUNT";
                worksheet.Range["A16"].Text = "Cabrales Cheese";
                worksheet.Range["A17"].Text = "Chocos";
                worksheet.Range["A18"].Text = "Pasta";
                worksheet.Range["A19"].Text = "Cereals";
                worksheet.Range["A20"].Text = "Ice Cream";

                worksheet.Range["C16"].Number = 3;
                worksheet.Range["C17"].Number = 2;
                worksheet.Range["C18"].Number = 1;
                worksheet.Range["C19"].Number = 4;
                worksheet.Range["C20"].Number = 3;

                worksheet.Range["D16"].Number = 21;
                worksheet.Range["D17"].Number = 54;
                worksheet.Range["D18"].Number = 10;
                worksheet.Range["D19"].Number = 20;
                worksheet.Range["D20"].Number = 30;

                worksheet.Range["D23"].Text = "Total";

                //Apply number format
                worksheet.Range["D16:E22"].NumberFormat = "$.00";
                worksheet.Range["E23"].NumberFormat = "$.00";

                //Apply incremental formula for column Amount by multiplying Qty and UnitPrice
                application.EnableIncrementalFormula = true;
                worksheet.Range["E16:E20"].Formula = "=C16*D16";

                //Formula for Sum the total
                worksheet.Range["E23"].Formula = "=SUM(E16:E22)";

                //Apply borders
                worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].Color = ExcelKnownColors.Grey_25_percent;
                worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Grey_25_percent;
                worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].Color = ExcelKnownColors.Black;
                worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Black;

                //Apply font setting for cells with product details
                worksheet.Range["A3:E23"].CellStyle.Font.FontName = "Arial";
                worksheet.Range["A3:E23"].CellStyle.Font.Size = 10;
                worksheet.Range["A15:E15"].CellStyle.Font.Color = ExcelKnownColors.White;
                worksheet.Range["A15:E15"].CellStyle.Font.Bold = true;
                worksheet.Range["D23:E23"].CellStyle.Font.Bold = true;

                //Apply cell color
                worksheet.Range["A15:E15"].CellStyle.Color = Color.FromArgb(42, 118, 189);

                //Apply alignment to cells with product details
                worksheet.Range["A15"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
                worksheet.Range["C15:C22"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                worksheet.Range["D15:E15"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

                //Apply row height and column width to look good
                worksheet.Range["A1"].ColumnWidth = 36;
                worksheet.Range["B1"].ColumnWidth = 11;
                worksheet.Range["C1"].ColumnWidth = 8;
                worksheet.Range["D1:E1"].ColumnWidth = 18;
                worksheet.Range["A1"].RowHeight = 47;
                worksheet.Range["A2"].RowHeight = 15;
                worksheet.Range["A3:A4"].RowHeight = 15;
                worksheet.Range["A5"].RowHeight = 18;
                worksheet.Range["A6"].RowHeight = 29;
                worksheet.Range["A7"].RowHeight = 18;
                worksheet.Range["A8"].RowHeight = 15;
                worksheet.Range["A9:A14"].RowHeight = 15;
                worksheet.Range["A15:A23"].RowHeight = 18;


                //Saving the Excel to the MemoryStream 
                MemoryStream stream = new MemoryStream();

                workbook.SaveAs(stream);

                //Set the position as '0'.
                stream.Position = 0;

                //Download the Excel file in the browser
                FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/excel");

                fileStreamResult.FileDownloadName = "Output.xlsx";

                return fileStreamResult;
            }
        }

        public void UpdateExcelSheetData()
        {
            string fileName = @"file name with .xlsx extension and file path ";
            using (SpreadsheetDocument spreadSheet =
             SpreadsheetDocument.Open(fileName, true))
            {
                AddUpdateCellValue(spreadSheet, "test sheet1", 8, "A", "test data1");
                AddUpdateCellValue(spreadSheet, "test sheet2", 8, "B", "test data2");
                AddUpdateCellValue(spreadSheet, "test sheet3", 8, "A", "test data3");
            }
        }

        public void AddUpdateCellValue(SpreadsheetDocument spreadSheet, string sheetname,
         uint rowIndex, string columnName, string text)
        {
            // Opening document for editing            
            WorksheetPart worksheetPart =
             RetrieveSheetPartByName(spreadSheet, sheetname);
            if (worksheetPart != null)
            {
                Cell cell = InsertCellInSheet(columnName, (rowIndex + 1), worksheetPart);
                cell.CellValue = new CellValue(text);
                //cell datatype            
                cell.DataType =
                 new EnumValue<CellValues>(CellValues.String);
                // Save the worksheet.            
                worksheetPart.Worksheet.Save();
            }
        }
        //retrieve sheetpart            
        public WorksheetPart RetrieveSheetPartByName(SpreadsheetDocument document,
         string sheetName)
        {
            IEnumerable<Sheet> sheets =
             document.WorkbookPart.Workbook.GetFirstChild<Sheets>().
            Elements<Sheet>().Where(s => s.Name == sheetName);
            if (sheets.Count() == 0)
                return null;

            string relationshipId = sheets.First().Id.Value;
            WorksheetPart worksheetPart = (WorksheetPart)
            document.WorkbookPart.GetPartById(relationshipId);
            return worksheetPart;
        }

        //insert cell in sheet based on column and row index            
        public Cell InsertCellInSheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        {
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            string cellReference = columnName + rowIndex;
            Row row;
            //check whether row exist or not            
            //if row exist            
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            //if row does not exist then it will create new row            
            else
            {
                row = new Row()
                {
                    RowIndex = rowIndex
                };
                sheetData.Append(row);
            }
            //check whether cell exist or not            
            //if cell exist            
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
                return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
            //if cell does not exist            
            else
            {
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>())
                {
                    if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                    {
                        refCell = cell;
                        break;
                    }
                }
                Cell newCell = new Cell()
                {
                    CellReference = cellReference
                };
                row.InsertBefore(newCell, refCell);
                worksheet.Save();
                return newCell;
            }
        }

        // retrieve cell based on column and row index            
        public Cell RetreiveCell(Worksheet worksheet,
         string columnName, uint rowIndex)
        {
            Row row = RetrieveRow(worksheet, rowIndex);
            var newRow = new Row()
            {
                RowIndex = (uint)rowIndex + 1
            };
            //adding new row            
            worksheet.InsertAt(newRow, Convert.ToInt32(rowIndex + 1));
            //create cell with value            
            Cell cell = new Cell();
            cell.CellValue = new CellValue("");
            cell.DataType =
             new EnumValue<CellValues>(CellValues.String);
            newRow.AddAnnotation(cell);
            worksheet.Save();

            row = newRow;
            if (row == null)
                return null;
            return row.Elements<Cell>().Where(c => string.Compare(c.CellReference.Value, columnName +
             (rowIndex + 1), true) == 0).First();
        }

        // it will return a row based on worksheet and rowindex            
        public Row RetrieveRow(Worksheet worksheet, uint rowIndex)
        {
            return worksheet.GetFirstChild<SheetData>().
            Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
        }


    }
}
