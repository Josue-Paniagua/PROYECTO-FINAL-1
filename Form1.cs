using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Office.Interop.Word;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using DocumentFormat.OpenXml.Presentation;


namespace PROYECTOFINAL1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        public class GroqApiService
        {
            private readonly string _apiKey = "gsk_QtkxO";

            private readonly string[] _modelosDisponibles =
            {
        "llama3-70b-8192",

    };

            public async Task<string> ConsultarIA(string prompt)
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
                client.Timeout = TimeSpan.FromSeconds(30);

                Exception lastError = null;

                foreach (var modelo in _modelosDisponibles)
                {
                    try
                    {
                        var requestBody = new
                        {
                            model = modelo,
                            messages = new[]
                            {
                        new { role = "user", content = prompt }
                    },
                            temperature = 0.7,
                            max_tokens = 1024
                        };

                        var response = await client.PostAsJsonAsync(
                            "https://api.groq.com/openai/v1/chat/completions",
                            requestBody);

                        var rawJson = await response.Content.ReadAsStringAsync();

                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception($"Error con modelo {modelo}: {rawJson}");
                        }

                        var content = JsonDocument.Parse(rawJson).RootElement;

                        if (content.TryGetProperty("choices", out JsonElement choices) &&
                            choices.GetArrayLength() > 0 &&
                            choices[0].TryGetProperty("message", out JsonElement message) &&
                            message.TryGetProperty("content", out JsonElement resultText))
                        {
                            return resultText.GetString();
                        }

                        throw new Exception("Formato de respuesta inesperado");
                    }
                    catch (Exception ex)
                    {
                        lastError = ex;
                        continue; // Intentar con el siguiente modelo
                    }
                }

                throw new Exception($"Todos los modelos fallaron. Último error: {lastError?.Message}");
            }
        }
        // ... (Resto del código permanece igual: DatabaseService, WordGenerator, PowerPointGenerator)

        private async void btnConsultar_Click(object sender, EventArgs e)
        {
            string prompt = txtPrompt.Text.Trim();
            if (string.IsNullOrEmpty(prompt))
            {
                MessageBox.Show("Por favor ingrese un tema o pregunta.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnConsultar.Enabled = false;
            rtbResultado.Text = "Consultando la IA... Por favor espere.";

            try
            {
                var iaService = new GroqApiService();
                string resultado = await iaService.ConsultarIA(prompt);
                rtbResultado.Text = resultado;
                GuardarEnBaseDeDatos(prompt, resultado);


            }
            catch (Exception ex)
            {
                rtbResultado.Text = $"Error al consultar la IA:\n{ex.Message}";

                // Mensaje más detallado en el MessageBox
                string mensajeError = ex.Message.Contains("Error de conexión")
                    ? $"No se pudo conectar al servicio de IA. Verifique:\n1. Su conexión a Internet\n2. Que la API key sea válida\n3. El estado del servicio en status.groq.com\n\nError técnico: {ex.Message}"
                    : $"Error: {ex.Message}";

                MessageBox.Show(mensajeError, "Error en la consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnConsultar.Enabled = true;
            }
        }

        private void btnAbrirWord_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rtbResultado.Text))
            {
                MessageBox.Show("No hay resultados para exportar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string filePath = Path.Combine(desktopPath, "Resultado_IA.docx");

                using (var doc = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
                {
                    // Crear estructura del documento
                    MainDocumentPart mainPart = doc.AddMainDocumentPart();
                    mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document();
                    Body body = new DocumentFormat.OpenXml.Wordprocessing.Body();

                    // Crear párrafo con el texto (usando el namespace completo)
                    var paragraph = new DocumentFormat.OpenXml.Wordprocessing.Paragraph();
                    var run = new DocumentFormat.OpenXml.Wordprocessing.Run();
                    run.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Text(rtbResultado.Text));

                    // Añadir formato opcional (negrita, fuente, tamaño)
                    run.PrependChild(
                        new DocumentFormat.OpenXml.Wordprocessing.RunProperties(
                            new DocumentFormat.OpenXml.Wordprocessing.Bold(),
                            new DocumentFormat.OpenXml.Wordprocessing.RunFonts() { Ascii = "Arial" },
                            new DocumentFormat.OpenXml.Wordprocessing.FontSize() { Val = "24" }
                        )
                    );

                    paragraph.AppendChild(run);
                    body.AppendChild(paragraph);
                    mainPart.Document.AppendChild(body);
                    mainPart.Document.Save();
                }

                MessageBox.Show($"Documento guardado en el Escritorio: Resultado_IA.docx", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar Word: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPrompt_TextChanged(object sender, EventArgs e)
        {

        }



        private void btnAbrirPPT_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rtbResultado.Text))
            {
                MessageBox.Show("No hay resultados para exportar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "Resultado_IA.pptx");

            using (PresentationDocument presentationDoc = PresentationDocument.Create(filePath, PresentationDocumentType.Presentation))
            {
                PresentationPart presentationPart = presentationDoc.AddPresentationPart();
                presentationPart.Presentation = new DocumentFormat.OpenXml.Presentation.Presentation();

                SlidePart slidePart = presentationPart.AddNewPart<SlidePart>();
                slidePart.Slide = new DocumentFormat.OpenXml.Presentation.Slide(new CommonSlideData(new ShapeTree()));

                Slide slide = slidePart.Slide;
                ShapeTree shapeTree = slide.CommonSlideData.ShapeTree;

                // Agrega título
                shapeTree.AppendChild(CreateTextShape("Resultado IA", 1000000, 500000));

                // Agrega contenido del resultado
                shapeTree.AppendChild(CreateTextShape(rtbResultado.Text, 1000000, 1500000));

                presentationPart.Presentation.SlideIdList = new SlideIdList();
                uint slideId = 256U;
                presentationPart.Presentation.SlideIdList.Append(new SlideId()
                {
                    Id = slideId,
                    RelationshipId = presentationPart.GetIdOfPart(slidePart)
                });

                presentationPart.Presentation.Save();
            }

            MessageBox.Show($"Presentación guardada en: {filePath}", "Exportación a PowerPoint", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private DocumentFormat.OpenXml.Presentation.Shape CreateTextShape(string text, int offsetX, int offsetY)
        {
            return new DocumentFormat.OpenXml.Presentation.Shape(
                new NonVisualShapeProperties(
                    new NonVisualDrawingProperties() { Id = 1U, Name = "TextBox" },
                    new NonVisualShapeDrawingProperties(new DocumentFormat.OpenXml.Drawing.ShapeLocks() { NoGrouping = true }),
                    new ApplicationNonVisualDrawingProperties()),
                new ShapeProperties(
                    new DocumentFormat.OpenXml.Drawing.Transform2D(
                        new DocumentFormat.OpenXml.Drawing.Offset() { X = offsetX, Y = offsetY },
                        new DocumentFormat.OpenXml.Drawing.Extents() { Cx = 6000000, Cy = 2000000 })),
                new TextBody(
                    new DocumentFormat.OpenXml.Drawing.BodyProperties(),
                    new DocumentFormat.OpenXml.Drawing.ListStyle(),
                    new DocumentFormat.OpenXml.Drawing.Paragraph(
                        new DocumentFormat.OpenXml.Drawing.Run(
                            new DocumentFormat.OpenXml.Drawing.Text() { Text = text }))));
        }
        private void GuardarEnBaseDeDatos(string prompt, string resultado)
        {
            string cadenaConexion = "Server=LAPTOP-0VH2N7ML\\SQLEXPRESS01;Database=TemaIA;Trusted_Connection=True;";
            string query = "INSERT INTO ConsultasIA (Prompt, Respuesta) VALUES (@prompt, @respuesta)";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@prompt", prompt);
                    comando.Parameters.AddWithValue("@respuesta", resultado);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            string carpeta = @"C:\Users\HP\OneDrive\Escritorio\documentos proyecto 1";
            string docPath = Path.Combine(carpeta, "Resultado_IA.docx");
            string pptPath = Path.Combine(carpeta, "Resultado_IA.pptx");

            if (File.Exists(docPath) && File.Exists(pptPath))
            {
                File.Copy(docPath, Path.Combine(carpeta, "Copia_Resultado_IA.docx"), true);
                File.Copy(pptPath, Path.Combine(carpeta, "Copia_Resultado_IA.pptx"), true);
                MessageBox.Show("Archivos copiados correctamente.");
            }
            else
            {
                MessageBox.Show("Uno o ambos archivos no existen. Genérelos primero.");
            }
        

    }
    }
}



    
    

    
