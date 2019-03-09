using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using AdmisionPS2019.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Globalization;


namespace AdmisionPS2019.Controllers
{
    public class MedicinaClinicaController : Controller
    {
        private masterEntities10 db = new masterEntities10();

   
        // GET: MedicinaClinica
        
      

       [HttpGet]
        public ActionResult MedicinaClinica()
        {
            A_PACIENTE oPaciente = new A_PACIENTE();
            //oPaciente = db.A_PACIENTE.SingleOrDefault();

            //return View(oPaciente);

            return View(oPaciente);
        }

       [HttpPost]
       public ActionResult MedicinaClinica([Bind(Include = "P_RUTPACIENTE")] A_PACIENTE oPaciente)
       {
           A_PACIENTE _Paciente = new A_PACIENTE();

            int rut = (Int32)oPaciente.P_RUTPACIENTE;

           A_PACIENTE licencia = db.A_PACIENTE.Find(rut);

           if (licencia == null)
           {
               return HttpNotFound();
           }

           return View(licencia);
       }

       [HttpGet]
       public FileStreamResult pdf(int id, String Nombre, String ApPaterno, String ApMaterno)
       {
           MemoryStream workStream = new MemoryStream();
           Document document = new Document();
           PdfWriter.GetInstance(document, workStream).CloseStream = false;

           string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
           baseUrl += "//Content//Images//logo.png";

           document.Open();
           string imageURL = baseUrl; //Server.MapPath("//Content//Images//logo.png"); // +"\\Content\\Images\\logo.png";

           iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageURL);
           //Resize image depend upon your need
           jpg.ScaleToFit(140f, 120f);
           //Give space before image
           jpg.SpacingBefore = 10f;
           //Give some space after the image
           jpg.SpacingAfter = 1f;
           jpg.Alignment = Element.ALIGN_LEFT;
           document.Add(jpg);
           document.Add(new Paragraph(" "));
           document.Add(new Paragraph(" "));
           document.Add(new Paragraph("                     ENTREGA A FONASA - ISAPRES Y OTROS ASEGURADORES"));
           document.Add(new Paragraph("                                                                 DE"));
           document.Add(new Paragraph("                          INFORMACIÓN MEDICA Y REGISTROS CLÍNICOS"));
           document.Add(new Paragraph(" "));
           document.Add(new Paragraph(" "));
           document.Add(new Paragraph("A        : Dirección Médica "));
           document.Add(new Paragraph("INSTITUTO OFTALMOLÓGICO PUERTA DEL SOL"));
           document.Add(new Paragraph(" "));
           document.Add(new Paragraph("PACIENTE Don(a) : " + Nombre.ToUpper() + " " + ApPaterno.ToUpper() + " " + ApMaterno.ToUpper()));
           document.Add(new Paragraph("                 Cédula nacional de identidad Nº : " + id.ToString()));
           document.Add(new Paragraph(" "));
           document.Add(new Paragraph("APODERADO Don(a) : "));
           document.Add(new Paragraph("                 Cédula nacional de identidad Nº _______________________"));
           document.Add(new Paragraph("                 Vinculo con paciente _______________________"));
           document.Add(new Paragraph(" "));
           document.Add(new Paragraph(" Teniendo conocimiento que la información medica es reservada, confidencial y considerada"));
           document.Add(new Paragraph(" como Dato Sensible; y el articulo 189 del DEL Nº 1 del año 2005 faculta a las intituciones"));
           document.Add(new Paragraph(" aseguradoras de salud y FONASA, que previo a resolver la procedencia y otorgamiento de un"));
           document.Add(new Paragraph(" beneficio de salud, pueda solicitar a cualquier prestador de salud que se les prporcionen "));
           document.Add(new Paragraph(" los antecedentes clínicos y médicos del paciente, como así tambien lo pueden requerir otros"));
           document.Add(new Paragraph(" aseguradores con los que a contratado o se ha contratado a su favor en calidad de asegurado"));
           document.Add(new Paragraph(" es que:"));
           document.Add(new Paragraph(" "));
           document.Add(new Paragraph(" En ejercicio de los derechos que confiere la ley Nº 20.584, declaro que _____ REVELO a esa"));
           document.Add(new Paragraph(" Dirección de esa obigación, y _____ AUTORIZO a que se proporcione todo antecedente de"));
           document.Add(new Paragraph(" salud que se les solicite, a la Aseguradora de Salud a la cual el paciente se "));
           document.Add(new Paragraph(" encuentre adscrito afiliado y/o beneficiario y/o a FONASA y/o asegurador con que"));
           document.Add(new Paragraph(" haya contratado o se haya contratado a su favor, calidad de aegurado;."));
           document.Add(new Paragraph(" "));
           document.Add(new Paragraph(" "));
           document.Add(new Paragraph(" "));
           document.Add(new Paragraph("                            Firma de Paciente o Representante"));
           document.Add(new Paragraph("                               RUT Nº ____________________"));
           document.Add(new Paragraph(" "));
           document.Add(new Paragraph(" "));
           document.Add(new Paragraph(" SANTIAGO," + DateTime.Now.Day.ToString() + " de " + DateTime.Now.ToString("MMMM") + " de " + DateTime.Now.Year.ToString()));
           //<h3> </h3>

           //document.Add(new Paragraph(DateTime.Now.ToString()));
           document.Close();

           byte[] byteInfo = workStream.ToArray();
           workStream.Write(byteInfo, 0, byteInfo.Length);
           workStream.Position = 0;

           return new FileStreamResult(workStream, "application/pdf");
       }
    }
}